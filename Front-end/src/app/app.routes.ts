import { Routes } from '@angular/router';
import { ProductList } from './features/products/product-list/product-list';
import { Login } from './features/auth/login/login';
import { ProductDetails } from './features/products/product-details/product-details';
import { Cart } from './features/cart/cart';
import { OrderSummary } from './features/orders/order-summary/order-summary';
import { NotFound } from './pages/not-found/not-found';
import { authGuard } from './core/guards/auth-guard';
import { ProductForm } from './features/products/product-form/product-form';
import { adminGuard } from './core/guards/admin-guard';

export const routes: Routes = [
  { path: '', component: ProductList, canActivate: [authGuard] },
  { path: 'products/add', component: ProductForm, canActivate: [adminGuard] },
  { path: 'products/edit/:id', component: ProductForm, canActivate: [adminGuard] },
  { path: 'products/:id', component: ProductDetails, canActivate: [authGuard], pathMatch:"full" },
  { path: 'login', component: Login },
  { path: 'cart', component: Cart, canActivate: [authGuard] },
  { path: 'order-summary/:id', component: OrderSummary, canActivate: [authGuard] },
  { path: '**', component: NotFound }
];