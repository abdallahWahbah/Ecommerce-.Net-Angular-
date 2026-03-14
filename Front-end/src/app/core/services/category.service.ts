import { inject, Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {

  private api = inject(ApiService);
  private endpoint = "categories";

    getCategories() {
      return this.api.get<Category[]>(this.endpoint);
    }
}
