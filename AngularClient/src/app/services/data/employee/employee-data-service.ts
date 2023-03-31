import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from '../../../dto/employee';
import { DataService } from '../data.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeDataService extends DataService<Employee> {

  constructor(http: HttpClient) {
    super(http, 'https://localhost:7194/api/employees');
  }
}
