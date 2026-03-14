import { inject, Injectable } from '@angular/core';
import { Product } from '../models/product';
import { ProductParam } from '../models/product-param';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root',
})
export class ProductService {

  private api = inject(ApiService);
  private endpoint = "products";

  getProducts(params?: ProductParam) {
    return this.api.get(this.endpoint, params);
  }

  getProductById(id: string) {
    return this.api.getById<Product>(this.endpoint, id);
  }

  createProduct(body: FormData) {
    return this.api.post<string>(this.endpoint, body);
  }

  updateProduct(id: string, body: FormData) {
    return this.api.put<void>(this.endpoint, id, body);
  }
}
