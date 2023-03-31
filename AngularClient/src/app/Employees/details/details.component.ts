import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../../dto/employee';
import { EmployeeDataService } from '../../services/data/employee/employee-data-service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class EmployeesDetailsComponent implements OnInit {
  constructor(private route: ActivatedRoute, private _employeeDataService: EmployeeDataService) { }
  employee: Employee | undefined

  ngOnInit(): void {
    this.route.paramMap
      .subscribe(params => {
        let id = Number.parseInt(params.get('id') as string);
        this._employeeDataService.get(id).subscribe(response => {
          this.employee = response;
        })
      })
  }


}
