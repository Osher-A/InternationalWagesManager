import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Employee } from '../common/Employee';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService extends DataService<Employee> {

  constructor(http: HttpClient) {
    super(http,'https://localhost:7194/api/employees');
  }
}
