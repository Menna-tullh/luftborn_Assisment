import { Component, OnInit } from '@angular/core';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CustomerService } from '../../../../services/customer.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { UpsertCustomerComponent } from '../upsert-customer/upsert-customer.component';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [
    MatPaginatorModule,
    MatTableModule,
    HttpClientModule,
    CommonModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    UpsertCustomerComponent
  ],
  providers: [CustomerService],
  templateUrl: './customer-list.component.html',
  styleUrl: './customer-list.component.css',
})
export class CustomerListComponent implements OnInit {
  customerList: any[] = [];
  pageIndex: number = 1;
  pageSize: number = 10;
  totalCount = 0;
  displayedColumns: string[] = ['firstName', 'lastName', 'phoneNumber', 'actions'];

  constructor(
    private customerService: CustomerService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.customerService.getCustomers(this.pageSize, this.pageIndex)
      .subscribe({
        next: (response: any) => {
          this.customerList = response.data || [];
          this.totalCount = response.totalCount || 0;
        },
        error: (error) => {
          console.error('Error loading customers:', error);
        }
      });
  }

  createCustomer() {
    const dialogRef = this.dialog.open(UpsertCustomerComponent, {
      width: 'auto',
      maxWidth: '90vw',
      minWidth: '600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadData(); // Refresh the list
      }
    });
  }

  onPage(event: any) {
    this.pageIndex = event.pageIndex + 1;
    this.loadData();
  }

  edit(customer: any) {
    const dialogRef = this.dialog.open(UpsertCustomerComponent, {
      width: 'auto',
      maxWidth: '90vw',
      minWidth: '600px',
      data: { customer }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadData(); // Refresh the list
      }
    });
  }

  deleteRow(customer: any) {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.customerService.deleteCustomer(customer.id).subscribe({
        next: () => {
          // Refresh the data after successful deletion
          this.loadData();
        },
        error: (error) => {
          console.error('Error deleting customer:', error);
        }
      });
    }
  }
}
