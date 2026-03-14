import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from "./layout/navbar/navbar";
import { SignalrService } from './core/services/signalr.service';
import { AuthService } from './core/services/auth.service';
import { Toast } from "./shared/components/toast/toast";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, Toast],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('Front-end');

  private signalr = inject(SignalrService);
  private authService = inject(AuthService);

  ngOnInit() {
    if (this.authService.isAuthenticated()) {
      this.signalr.startConnection();
    }
  }
}
