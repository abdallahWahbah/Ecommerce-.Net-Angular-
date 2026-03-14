import { Component, inject, signal } from '@angular/core';
import { OrderResponse } from '../../../core/models/order';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AngularMaterialModule } from '../../../shared/modules/angular-material.module';
import { OrderService } from '../../../core/services/order.service';
import { AuthService } from '../../../core/services/auth.service';
import { CustomButton } from "../../../shared/components/custom-button/custom-button";

@Component({
  selector: 'app-order-summary',
  imports: [CommonModule, AngularMaterialModule, CustomButton],
  templateUrl: './order-summary.html',
  styleUrl: './order-summary.scss',
})
export class OrderSummary {
  order!: OrderResponse;

  private orderService = inject(OrderService);
  private authService = inject(AuthService);
  private router = inject(Router);

  orders = signal<OrderResponse[]>([]);

  ngOnInit() {

    const userId = this.authService.getUserId()!;

    this.orderService.getUserOrders(userId).subscribe((res: any) => {
      this.orders.set(res);
    });

  }

  browseProducts(){
    this.router.navigate(['/']);
  }
}
