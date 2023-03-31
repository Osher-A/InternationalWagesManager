import { EmployeeDataService } from 'src/app/services/data/employee/employee-data-service';
import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/dto/employee';

@Component({
  selector: 'index',
  templateUrl: './index.component.html',
  styleUrls: ['../../app.component.css']
})
export class SalaryIndexComponent implements OnInit {
  employees: Employee[] = [];
  constructor(private employeeService: EmployeeDataService) { }

  ngOnInit(): void {
    this.employeeService.getAll().subscribe(results => {
      this.employees = results;
    })
  }

}
