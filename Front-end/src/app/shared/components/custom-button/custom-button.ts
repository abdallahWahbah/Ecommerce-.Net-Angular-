import { Component, Input } from '@angular/core';
import { AngularMaterialModule } from '../../modules/angular-material.module';

@Component({
  selector: 'app-custom-button',
  imports: [AngularMaterialModule],
  templateUrl: './custom-button.html',
  styleUrl: './custom-button.scss',
})
export class CustomButton {
  @Input() label = 'Submit';
  @Input() loading = false;
  @Input() disabled = false;
  @Input() type: 'button' | 'submit' = 'submit';
}
