import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { AngularMaterialModule } from '../../modules/angular-material.module';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormField } from '../../../core/models/form-field';
import { CustomButton } from '../custom-button/custom-button';

@Component({
  selector: 'app-dynamic-form',
  imports: [AngularMaterialModule, ReactiveFormsModule, CustomButton],
  templateUrl: './dynamic-form.html',
  styleUrl: './dynamic-form.scss',
})
export class DynamicForm {
  @Input() fields: FormField[] = [];
  @Input() submitLabel = 'Submit';
  @Input() initialData: any;
  @Output() formSubmit = new EventEmitter<any>();
  form!: FormGroup;
  loading = false;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    const controls: any = {};
    this.fields.forEach(field => {
      controls[field.name] = [field.initialValue ?? '', field.validations || []];
      // controls[field.name] = ['', field.validations || []];
    });
    this.form = this.fb.group(controls);
    if (this.initialData) {
      this.form.patchValue(this.initialData);
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['initialData'] && this.initialData && this.form) {
      this.form.patchValue(this.initialData);
    }
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.loading = true;
    this.formSubmit.emit(this.form.value);
    this.loading = false;
  }

  onFileChange(event: any, fieldName: string) {
    const file = event.target.files[0];
    this.form.patchValue({
      [fieldName]: file
    });
  }
}
