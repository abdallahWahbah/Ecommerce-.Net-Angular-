import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = 'https://localhost:7184/api';

  constructor(private http: HttpClient) {}

  get<T>(endpoint: string, params?: Record<string, any>) {
    let httpParams = new HttpParams();

    if (params) {
      Object.keys(params).forEach((key) => {
        const value = params[key];
        if (value !== null && value !== undefined && value !== '') {
          httpParams = httpParams.set(key, String(value));
        }
      });
    }

    return this.http.get<T>(`${this.baseUrl}/${endpoint}`, { params: httpParams });
  }

  getById<T>(endpoint: string, id: string) {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}/${id}`);
  }

  post<T>(endpoint: string, body: any) {
    return this.http.post<T>(`${this.baseUrl}/${endpoint}`, body);
  }

  put<T>(endpoint: string, id: string, body: any) {
    return this.http.put<T>(`${this.baseUrl}/${endpoint}/${id}`, body);
  }

  putCustom<T>(endpoint: string, body: any = {}) {
    return this.http.put<T>(`${this.baseUrl}/${endpoint}`, body);
  }
}
