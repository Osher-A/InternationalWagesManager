import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from '../../dto/employee';
import { EmployeeDataService } from '../../services/data/employee/employee-data-service';

@Component({
  selector: 'add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class EmployeesAddComponent implements OnInit {

  constructor(private _employeeDataService: EmployeeDataService, private _router: Router) { }
  employee: Employee = new Employee()

  ngOnInit(): void {

  }

  OnValidSubmit() {
    this._employeeDataService.post(this.employee)
      .subscribe(then => {
        this._router.navigate(["/"]);
      })
  }

}
