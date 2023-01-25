import { Component, OnInit } from '@angular/core';
import { Employee } from '../../dto/employee';
import { EmployeeDataService } from '../../services/employee-data-service';

@Component({
  selector: 'index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class EmployeesIndexComponent implements OnInit {
  employees: Employee[] = [];
  constructor(private employeeService: EmployeeDataService) { }

  ngOnInit(): void {
    this.employeeService.getAll().subscribe(response => {
      this.employees = response;
    })
  }
  deleteEmployee(employee: Employee) {
    this.employeeService.delete(employee.id)
      .subscribe(then =>
        this.ngOnInit())
  }
}
