import { Component, inject, signal } from '@angular/core';
import { ProductService } from '../../../core/services/product.service';
import { CategoryService } from '../../../core/services/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../../../core/models/category';
import { AngularMaterialModule } from '../../../shared/modules/angular-material.module';
import { ADD_PRODUCT_FIELDS } from './add-product.fields';
import { FormField } from '../../../core/models/form-field';
import { DynamicForm } from "../../../shared/components/dynamic-form/dynamic-form";
import { MatSnackBar } from '@angular/material/snack-bar';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-product-form',
  imports: [AngularMaterialModule, DynamicForm],
  templateUrl: './product-form.html',
  styleUrl: './product-form.scss',
})
export class ProductForm {

  constructor(
    private snack: MatSnackBar
  ) {}

  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  fields: FormField[] = ADD_PRODUCT_FIELDS;
  productData: any = null;
  categories = signal<Category[]>([]);
  loading = signal(true);
  productId: string | null = null;
  isEditMode = false;

  ngOnInit(){

    this.productId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.productId;
    if(this.isEditMode){
      forkJoin({
        categories: this.categoryService.getCategories(),
        product: this.productService.getProductById(this.productId!)
      }).subscribe(({categories, product}) => {
        this.categories.set(categories);
        const categoryField = this.fields.find(f => f.name === 'categoryId');
        if (categoryField) {
          categoryField.options = categories.map(c => ({
            value: c.id,
            label: c.name
          }));
        }

        const currentProductCategory = categories.find(c => c.name === product.categoryName);
        this.productData = {
          ...product,
          categoryId: currentProductCategory?.id
        };
        this.loading.set(false);
      });
    }
    else {
      this.categoryService.getCategories().subscribe(categories => {
        this.categories.set(categories);
        const categoryField = this.fields.find(f => f.name === 'categoryId');
        if (categoryField) {
          categoryField.options = categories.map(c => ({
            value: c.id,
            label: c.name
          }));
        }
        this.loading.set(false);
      });
    }
  }

  loadProduct(){
    this.productService.getProductById(this.productId!)
    .subscribe(product => {
      this.productData = product;
    });
  }

  onSubmit(data:any){
    const formData = new FormData();
    Object.keys(data).forEach(key=>{
      let value = data[key];
      if (key === 'price' || key === 'stockQuantity') {
        value = Number(value);
      }
      formData.append(key, value);
      formData.append("id", this.productId!);
    });
    const request: any = this.isEditMode
      ? this.productService.updateProduct(this.productId!, formData)
      : this.productService.createProduct(formData);

    request.subscribe({
      next: () => {
        this.snack.open(this.isEditMode ? 'Product updated' : 'Product created', 'Close', { 
          duration: 3000 
        });
        this.router.navigate(['/']);
      },
      error: (err: any) => {
        const message = err?.error?.errors?.[0] || 'Something went wrong';
        this.snack.open(message, 'Close', {
          duration: 4000
        });
      }
    });
  }
}
