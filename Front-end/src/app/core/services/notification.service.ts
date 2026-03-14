import { inject, Injectable, signal } from '@angular/core';
import { ApiService } from './api.service';
import { Notification } from '../models/notification';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {

  private api = inject(ApiService);
  private endpoint = "notifications";
  unreadCount = signal(0);

  getNotifications() {
    return this.api.get<Notification[]>(this.endpoint);
  }

  getUnreadCount() {
    return this.api.get<number>(this.endpoint + "/unread-count")
  }

  markAsRead(id: string) {
    return this.api.putCustom<void>(`${this.endpoint}/${id}/read`);
  }

  refreshUnreadCount() {
    this.getUnreadCount().subscribe(c => this.unreadCount.set(c));
  }
}
