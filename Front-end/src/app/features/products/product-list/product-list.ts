import { Component, inject, signal } from '@angular/core';
import { Product } from '../../../core/models/product';
import { ProductService } from '../../../core/services/product.service';
import { AngularMaterialModule } from '../../../shared/modules/angular-material.module';
import { CustomButton } from '../../../shared/components/custom-button/custom-button';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { CategoryService } from '../../../core/services/category.service';
import { Category } from '../../../core/models/category';
import { AuthService } from '../../../core/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-list',
  imports: [AngularMaterialModule, CustomButton, CommonModule],
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss',
})
export class ProductList {
  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  authService = inject(AuthService);
  private router = inject(Router);

  env = environment;
  products = signal<Product[]>([]);
  categories = signal<Category[]>([]);
  loading = signal(false);
  totalCount = signal(0);
  pageNumber = signal(1);
  pageSize = signal(4);
  search = signal('');
  categoryId = signal('');
  sortBy = signal<string | undefined>(undefined);
  sortDirection = signal<'asc' | 'desc' | undefined>(undefined);


  ngOnInit() {
    this.loadProducts();
    this.loadCategories();
  }

  loadProducts() {
    // this.loading.set(true);

    this.productService.getProducts({
      pageNumber: this.pageNumber(),
      pageSize: this.pageSize(),
      search: this.search(),
      categoryId: this.categoryId(),
      sortBy: this.sortBy(),
      sortDirection: this.sortDirection()
    })
    .subscribe({
      next: (res: any) => {
        this.products.set(res.items);
        this.totalCount.set(res.totalCount);
      },
      complete: () => {
        this.loading.set(false);
      }
    });
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (res) => {
        this.categories.set(res);
      }
    });
  }

  getImage(url: string) {
    return this.env.apiUrl + url;
  }

  totalPages = () => Math.ceil(this.totalCount() / this.pageSize());

  changePage(page: number) {
    if (page < 1 || page > this.totalPages()) return;
    this.pageNumber.set(page);
    this.loadProducts();
  }

  onSearch(value: string) {
    this.search.set(value);
    this.pageNumber.set(1);
    this.loadProducts();
  }

  onCategoryChange(categoryId: string) {
    this.categoryId.set(categoryId);
    this.pageNumber.set(1);
    this.loadProducts();
  }

  onSortChange(sort: string) {
    if (sort === 'priceAsc') {
      this.sortBy.set('price');
      this.sortDirection.set('asc');
    }
    if (sort === 'priceDesc') {
      this.sortBy.set('price');
      this.sortDirection.set('desc');
    }
    if (sort === 'nameAsc') {
      this.sortBy.set('name');
      this.sortDirection.set('asc');
    }
    if (sort === 'nameDesc') {
      this.sortBy.set('name');
      this.sortDirection.set('desc');
    }
    this.loadProducts();
  }

  navigateToDetailsOrEdit(product: Product) {
    if (this.authService.getRole() === "Admin") {
      this.router.navigate(['/products/edit', product.id]);
    } else {
      this.router.navigate(['/products', product.id]);
    }
  }

  navigateToAddProduct(){
    this.router.navigate(['/products/add']);
  }
}
