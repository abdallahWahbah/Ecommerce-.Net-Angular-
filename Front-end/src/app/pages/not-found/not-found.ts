import { Component } from '@angular/core';
import { AngularMaterialModule } from '../../shared/modules/angular-material.module';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-not-found',
  imports: [AngularMaterialModule, RouterLink],
  templateUrl: './not-found.html',
  styleUrl: './not-found.scss',
})
export class NotFound {}
