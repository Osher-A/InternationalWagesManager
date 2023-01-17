import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map, catchError } from 'rxjs/operators'
import { Employee } from '../../common/Employee';
import { DataService } from '../../services/data.service';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  employees: Employee[] = [];
  url: string = 'https://localhost:7194/api/employees'
  constructor(private employeeService: EmployeeService, private router: Router) { }

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
