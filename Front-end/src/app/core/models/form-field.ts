export interface FormField {
    type: 'text' | 'email' | 'password' | 'number' | 'select' | 'file';
    name: string;
    label: string;
    placeholder: string;
    validations: any[];
    initialValue?: any;
    options?: { value: any; label: string }[];
}