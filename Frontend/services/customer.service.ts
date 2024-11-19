import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../environments/environment';

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
    
    return this.http.get(this.baseUrl, { params });
  }
} 

function Injectable(arg0: { providedIn: string; }): (target: typeof CustomerService, context: ClassDecoratorContext<typeof CustomerService>) => void | typeof CustomerService {
    throw new Error("Function not implemented.");
}

