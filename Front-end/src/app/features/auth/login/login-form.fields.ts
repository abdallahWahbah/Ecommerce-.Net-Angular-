import { Validators } from '@angular/forms';
import { FormField } from '../../../core/models/form-field';

export const LOGIN_FIELDS: FormField[] = [
  {
    type: 'email',
    name: 'email',
    label: 'Email',
    placeholder: 'Enter your email',
    validations: [
      Validators.required,
      Validators.email
    ],
    initialValue: 'admin@admin.com',
  },
  {
    type: 'password',
    name: 'password',
    label: 'Password',
    placeholder: 'Enter your password',
    validations: [
      Validators.required
    ],
    initialValue: 'admin123',
  }
];