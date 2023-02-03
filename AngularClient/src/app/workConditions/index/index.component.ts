import { Component, OnInit } from '@angular/core';
import { Employee } from '../../dto/employee';
import { EmployeeDataService } from '../../services/employee-data-service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['../../app.component.css']
})
export class WorkConditionsIndexComponent implements OnInit {
  employees: Employee[] = [];
  constructor(private _employeeService: EmployeeDataService) { }

  ngOnInit(): void {
    this._employeeService.getAll().subscribe(results => {
      this.employees = results;
    })
  }

}
