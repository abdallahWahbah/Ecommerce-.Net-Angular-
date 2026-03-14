import { Validators } from '@angular/forms';
import { FormField } from '../../../core/models/form-field';

export const ADD_PRODUCT_FIELDS: FormField[] = [
    {
        name: 'name',
        label: 'Product Name',
        type: 'text',
        placeholder: 'Enter product name',
        validations: [Validators.required]
    },
    {
        name: 'description',
        label: 'Description',
        type: 'text',
        placeholder: 'Enter description',
        validations: [Validators.required]
    },
    {
        name: 'price',
        label: 'Price',
        type: 'number',
        placeholder: 'Enter price',
        validations: [Validators.required]
    },
    {
        name: 'stockQuantity',
        label: 'Stock Quantity',
        type: 'number',
        placeholder: 'Enter stock',
        validations: [Validators.required]
    },
    {
        name: 'categoryId',
        label: 'Category',
        type: 'select',
        placeholder: 'Select Category',
        validations: [Validators.required],
        options: [],
    },
    {
        name: 'image',
        label: '',
        type: 'file',
        placeholder: 'Upload Image',
        validations: [Validators.required]
    }
];