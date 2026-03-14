import { Injectable, signal } from '@angular/core';
import { Toast, ToastType } from '../models/toast';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private counter = 0;

  toasts = signal<Toast[]>([]);

  show(message: string, type: ToastType = 'info') {

    const toast: Toast = {
      id: ++this.counter,
      message,
      type
    };
    
    this.toasts.update(t => [...t, toast]);

    setTimeout(() => {
      this.remove(toast.id);
    }, 4000);
  }

  remove(id: number) {
    this.toasts.update(t => t.filter(x => x.id !== id));
  }
}
