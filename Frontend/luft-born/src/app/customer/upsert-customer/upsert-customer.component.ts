import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CustomerService } from '../../../../services/customer.service';

@Component({
  selector: 'app-upsert-customer',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './upsert-customer.component.html',
  styleUrl: './upsert-customer.component.css'
})
export class UpsertCustomerComponent implements OnInit {
  customerForm: FormGroup;
  isEditMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<UpsertCustomerComponent>,
    private customerService: CustomerService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.customerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      address: ['',Validators.required],
      phoneNumber: ['', [Validators.pattern(/^\+201[0-9]{9}$/)]],
      email: ['', [ Validators.pattern(/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+$/)]],
    });
  }

  ngOnInit() {
    if (this.data?.customer) {
      this.isEditMode = true;
      this.customerForm.patchValue(this.data.customer);
    }
  }

  onSubmit() {
    if (this.customerForm.valid) {
      if (this.isEditMode) {
        const updateData = {
          ...this.customerForm.value,
          id: this.data.customer.id,
          isActive: true
        };
        this.customerService.updateCustomer(updateData).subscribe({
          next: (response) => {
            this.dialogRef.close(response);
          },
          error: (error) => console.error('Error updating customer:', error)
        });
      } else {
        this.customerService.createCustomer(this.customerForm.value).subscribe({
          next: (response) => {
            this.dialogRef.close(response);
          },
          error: (error) => console.error('Error creating customer:', error)
        });
      }
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
