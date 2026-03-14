import { computed, Injectable, signal } from '@angular/core';
import { CartItem } from '../models/cart-item';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root',
})
export class CartService {

    private storageKey = 'cart';
    cartItems = signal<CartItem[]>(this.loadCart());

    total = computed(() =>
        this.cartItems().reduce((sum, item) => sum + item.price * item.quantity, 0)
    );

    discount = computed(() =>
        this.total() > 1000 ? this.total() * 0.1 : 0
    );

    totalToPay = computed(() =>
        this.total() - this.discount()
    );


    addToCart(product: Product) {
        const items = this.cartItems();
        const productExists = items.find(i => i.productId === product.id);

        if (productExists) {
            if (productExists.quantity >= productExists.stock) return;
            productExists.quantity++;
            this.cartItems.set([...items]);
        } else {
            this.cartItems.set([ ...items, {
                productId: product.id,
                name: product.name,
                price: product.price,
                imageUrl: product.imageUrl,
                quantity: 1,
                stock: product.stockQuantity
            }]);
        }
        this.saveCart();
    }

    increase(productId: string) {
        const items = this.cartItems();
        const item = items.find(i => i.productId === productId);

        if (!item) return;
        if (item.quantity >= item.stock) return;

        item.quantity++;
        this.cartItems.set([...items]);
        this.saveCart();
    }

    decrease(productId: string) {
        const items = this.cartItems();
        const item = items.find(i => i.productId === productId);

        if (!item) return;
        if (item.quantity === 1) {
            this.removeItem(productId);
            return;
        }

        item.quantity--;
        this.cartItems.set([...items]);
        this.saveCart();
    }

    getCartItems() {
        return this.cartItems;
    }

    private saveCart() {
        localStorage.setItem(this.storageKey, JSON.stringify(this.cartItems()));
    }

    private loadCart(): CartItem[] {
        const data = localStorage.getItem(this.storageKey);
        return data ? JSON.parse(data) : [];
    }

    removeItem(productId: string) {
        const updated = this.cartItems().filter(x => x.productId !== productId);
        this.cartItems.set(updated);
        this.saveCart();
    }

    clearCart(){
        this.cartItems.set([]);
        localStorage.removeItem(this.storageKey);
    }
}