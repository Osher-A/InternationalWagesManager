import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { WorkConditions } from '../../dto/workConditions';
import { EmployeeDataService } from '../../services/employee-data-service';
import { MessageService } from '../../services/message.service';
import { WorkCDataService } from '../../services/work-cdata-service.';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['../../app.component.css']
})
export class WorkConditionsDetailsComponent implements OnInit {
  workConditions = new BehaviorSubject<WorkConditions[]>([]);
  _workCService: WorkCDataService | undefined;
  nameOfEmployee: string = "";

  constructor(private _http: HttpClient, private _route: ActivatedRoute, private _employeeDataService: EmployeeDataService,
    private _messageService: MessageService) { }

  ngOnInit(): void {
    let param = this._route.snapshot.params['id'];
    this._workCService = new WorkCDataService(this._http, `/employee/${param}`);
    this._workCService.getAll().subscribe(response => {
      this.workConditions.next(response);
    })
    this._employeeDataService.get(param).subscribe(response => {
      this.nameOfEmployee = response.firstName;
    })

  }

  onEdit(workC: WorkConditions) {

  }

  async onDelete(workC: WorkConditions) {
    if (await this._messageService.usersConfirmation()) {
      this._workCService = new WorkCDataService(this._http);
      this._workCService.delete((workC as WorkConditions).id as number).subscribe(async response => {
        this.workConditions.next(this.workConditions.value.filter(wc => wc != workC));
      })
    }
  }
}
