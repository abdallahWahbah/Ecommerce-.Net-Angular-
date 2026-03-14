import { AuthService } from './auth.service';
import { Injectable, inject } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ApiService } from './api.service';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {

  private authService = inject(AuthService);
  private notificationService = inject(NotificationService);
  private api = inject(ApiService);
  private hub!: signalR.HubConnection;

  startConnection() {
    this.hub = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7184/notificationHub', {
        accessTokenFactory: () => this.authService.getToken() ?? '',
        withCredentials: true
      })
      .withAutomaticReconnect()
      .build();

    this.hub.start()
      .catch(err => console.error('SignalR Error:', err));

    this.listenForNotifications();
  }

  private listenForNotifications() {
    this.hub.on('ReceiveNotification', (notification) => {
      console.log('notification received', notification);
      this.notificationService.refreshUnreadCount();

    });
  }
}
