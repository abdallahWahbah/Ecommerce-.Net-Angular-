import { Component, inject } from '@angular/core';
import { AngularMaterialModule } from '../../shared/modules/angular-material.module';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';
import { Notification } from '../../core/models/notification';
import { NotificationService } from '../../core/services/notification.service';
import { forkJoin } from 'rxjs';
import { ToastService } from '../../core/services/toast.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, AngularMaterialModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar {
  
  authService = inject(AuthService);
  notificationService = inject(NotificationService);
  toastService = inject(ToastService);
  
  
  unreadCount: any = this.notificationService.unreadCount;
  notifications: Notification[] = [];

  ngOnInit() {
    this.loadUnreadCount();
    this.notificationService.refreshUnreadCount();
  }

  loadUnreadCount() {
    this.notificationService.getUnreadCount().subscribe(count => this.unreadCount = count);
  }

  loadNotifications() {

    this.notificationService.getNotifications()
    .subscribe(data => {
      this.notifications = data;

      const unread = data.filter(n => !n.isRead);
      if (unread.length === 0) return;

      const requests = unread.map(n =>
        this.notificationService.markAsRead(n.id)
      );

      forkJoin(requests).subscribe(() => {
        this.notificationService.unreadCount.set(0);
      });
    });
  }

  handleLogout(){
    this.authService.logout();
    this.toastService.show("See you soon!", "warning");

  }
}
