import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from '../../common/Employee';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {

  constructor(private _employeeDataService: EmployeeService, private _router: Router) { }
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
