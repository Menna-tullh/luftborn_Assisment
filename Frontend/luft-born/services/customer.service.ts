import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { catchError, tap } from 'rxjs/operators';

interface CreateCustomerCommand {
  firstName?: string;
  lastName?: string;
  address?: string;
  phoneNumber?: string;
  email?: string;
}

interface UpdateCustomerCommand {
  id?: string;
  firstName?: string;
  lastName?: string;
  address?: string;
  phoneNumber?: string;
  email?: string;
  isActive: boolean;
}

interface PaginatedRequest {
  pageSize: number;
  pageIndex: number;
}

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private baseUrl = `${environment.apiUrl}/api/Customer`;

  constructor(private http: HttpClient) {}

  // API Methods
  createCustomer(customer: CreateCustomerCommand): Observable<any> {
    return this.http.post(this.baseUrl, customer);
  }

  updateCustomer(customer: UpdateCustomerCommand): Observable<any> {
    return this.http.put(this.baseUrl, customer);
  }

  deleteCustomer(id: string): Observable<any> {
    const params = new HttpParams().set('Id', id);
    return this.http.delete(this.baseUrl, { params });
  }
  getCustomers(pageSize: number, pageIndex: number): Observable<any> {
    const params = new HttpParams()
      .set('PageSize', pageSize.toString())
      .set('PageIndex', pageIndex.toString());
    
    const headers = new HttpHeaders({
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    });
    
    console.log('Calling API:', this.baseUrl);

    return this.http.get(this.baseUrl, { params, headers })
      .pipe(
        tap(response => console.log('API Response:', response)),
        catchError(error => {
          console.error('API Error:', error);
          throw error;
        })
      );
  }
} 

