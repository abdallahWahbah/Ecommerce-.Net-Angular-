import { Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from './api.service';
import { LoginRequestDto } from '../models/login-request-dto';
import { LoginResponseDto } from '../models/login-response-dto';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private tokenKey = 'token';
  isAuthenticated = signal<boolean>(!!localStorage.getItem(this.tokenKey));

  constructor(private apiService: ApiService, private router: Router) {}

  login(credentials: LoginRequestDto) {
    return this.apiService.post<LoginResponseDto>('auth/login', credentials);
  }

  saveCredentials(res: LoginResponseDto) {
    localStorage.setItem(this.tokenKey, res.token);
    localStorage.setItem("userId", res.userId);
    localStorage.setItem("role", res.role);
    this.isAuthenticated.set(true);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem("userId");
    localStorage.removeItem("role");
    this.isAuthenticated.set(false);
    this.navigateToLogin();
  }

  navigateToLogin() {
    this.router.navigate(['/login']);
  }

  getToken(){
    return localStorage.getItem(this.tokenKey);
  }

  getUserId() {
    return localStorage.getItem("userId");
  }

  getRole() {
    return localStorage.getItem("role");
  }
}
