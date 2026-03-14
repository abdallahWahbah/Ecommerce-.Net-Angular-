import { inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { CreateOrderRequest, OrderResponse } from '../models/order';

@Injectable({
  providedIn: 'root',
})
export class OrderService {

  private api = inject(ApiService);

  createOrder(request: CreateOrderRequest) {
    return this.api.post<OrderResponse>('orders', request);
  }

  getUserOrders(userId: string) {
    return this.api.get(`orders/user/${userId}`);
  }
}
