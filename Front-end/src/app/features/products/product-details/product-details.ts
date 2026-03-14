import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { Product } from '../../../core/models/product';
import { environment } from '../../../../environments/environment';
import { AngularMaterialModule } from '../../../shared/modules/angular-material.module';
import { CustomButton } from "../../../shared/components/custom-button/custom-button";
import { CartService } from '../../../core/services/cart.service';

@Component({
  selector: 'app-product-details',
  imports: [AngularMaterialModule, CustomButton],
  templateUrl: './product-details.html',
  styleUrl: './product-details.scss',
})
export class ProductDetails {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private productService = inject(ProductService);
  private cartService = inject(CartService);

  product = signal<Product | null>(null);

  env = environment;

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');

    this.productService.getProductById(String(id)).subscribe({
      next: (res) => {
        this.product.set(res);
      },
      error: () => {
        console.log("Error");
      }
    });
  }

  addToCart(product: Product){
    this.cartService.addToCart(product);
    this.router.navigate(['/cart']);
  }

  getImage(url: string) {
    return this.env.apiUrl + url;
  }
}
