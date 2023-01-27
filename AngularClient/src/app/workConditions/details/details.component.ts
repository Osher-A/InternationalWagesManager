import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { WorkConditions } from '../../dto/workConditions';
import { EmployeeDataService } from '../../services/employee-data-service';
import { WorkCDataService } from '../../services/work-cdata-service.';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class WorkConditionsDetailsComponent implements OnInit {

  workConditions: WorkConditions[] = [];
  _workCService: WorkCDataService | undefined;
  nameOfEmployee: string = "";

  constructor(private _http: HttpClient, private _route: ActivatedRoute, private _employeeDataService: EmployeeDataService) { }

  ngOnInit(): void {
    let param = this._route.snapshot.params['id'];
    this._workCService = new WorkCDataService(this._http, `/employee/${param}`);
    this._workCService.getAll().subscribe(response => {
      this.workConditions = response;
    })
    this._employeeDataService.get(param).subscribe(response => {
      this.nameOfEmployee = response.firstName;
      console.log(this.nameOfEmployee);
    })

  }

  onEdit(workC: WorkConditions) {

  }

  onDelete(workC: WorkConditions) {

  }
}
