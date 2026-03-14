import { Component, inject, ViewChild } from '@angular/core';
import { Validators } from '@angular/forms';
import { DynamicForm } from '../../../shared/components/dynamic-form/dynamic-form';
import { FormField } from '../../../core/models/form-field';
import { LOGIN_FIELDS } from './login-form.fields';
import { AngularMaterialModule } from '../../../shared/modules/angular-material.module';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LoginResponseDto } from '../../../core/models/login-response-dto';
import { ToastService } from '../../../core/services/toast.service';

@Component({
  selector: 'app-login',
  imports: [DynamicForm, AngularMaterialModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {


  fields: FormField[] = LOGIN_FIELDS;

  // @ViewChild(DynamicForm) formComponent!: DynamicForm;
  private toastService = inject(ToastService);

  constructor(
    private auth: AuthService,
    private router: Router,
    // private snack: MatSnackBar
  ) {}

  onSubmit(data: any) {

    this.auth.login(data).subscribe({
      next: (res: LoginResponseDto) => {
        this.toastService.show("Logged in successfully","success");
        this.auth.saveCredentials(res);
        this.router.navigate(['/']);
      },
      error: (err) => {
        const message = err?.error?.errors[0] || 'Login failed';
        this.toastService.show(message,"error");

        // this.snack.open(message, 'Close', {
        //   duration: 4000
        // });
      }
    });

  }
}
