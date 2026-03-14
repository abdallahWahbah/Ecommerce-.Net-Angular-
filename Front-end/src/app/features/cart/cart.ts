import { Component, inject } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
import { environment } from '../../../environments/environment';
import { AngularMaterialModule } from '../../shared/modules/angular-material.module';
import { CommonModule } from '@angular/common';
import { CustomButton } from "../../shared/components/custom-button/custom-button";
import { Router } from '@angular/router';
import { OrderService } from '../../core/services/order.service';
import { AuthService } from '../../core/services/auth.service';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-cart',
  imports: [CommonModule, AngularMaterialModule, CustomButton],
  templateUrl: './cart.html',
  styleUrl: './cart.scss',
})
export class Cart {

  private cartService = inject(CartService);
  private orderService = inject(OrderService);
  private authService = inject(AuthService);
  private toastService = inject(ToastService);
  private router = inject(Router);

  env = environment;
  cartItems = this.cartService.getCartItems();
  total = this.cartService.total;
  discount = this.cartService.discount;
  totalToPay = this.cartService.totalToPay;

  increase(id: string) {
    this.cartService.increase(id);
  }

  decrease(id: string) {
    this.cartService.decrease(id);
  }

  remove(id: string) {
    this.cartService.removeItem(id);
  }

  checkout(){
    const userId = this.authService.getUserId()!;
    const items = this.cartItems().map(i => ({
      productId: i.productId,
      quantity: i.quantity
    }));
    this.orderService.createOrder({userId, items})
    .subscribe({
      next: (order) => {
        this.toastService.show("Order placed successfully","success");
        this.cartService.clearCart();
        this.router.navigate(['/order-summary', userId]);
      }
    });

  }
}
