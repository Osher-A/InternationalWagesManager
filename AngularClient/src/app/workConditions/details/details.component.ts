import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { WorkConditions } from '../../dto/workConditions';
import { EmployeeDataService } from '../../services/employee-data-service';
import { MessageService } from '../../services/message.service';
import { WorkCDataService } from '../../services/work-cdata-service.';
import { MAT_DATE_LOCALE } from '@angular/material/core';


@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['../../app.component.css'],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }
  ],
})
export class WorkConditionsDetailsComponent implements OnInit {
  private _workCService: WorkCDataService;
  private _updatedDate: string | null = null;
  workConditions = new BehaviorSubject<WorkConditions[]>([]);
  nameOfEmployee: string = "";

  constructor(private _http: HttpClient, private _route: ActivatedRoute, private _employeeDataService: EmployeeDataService,
    private _messageService: MessageService) {
    this._workCService = new WorkCDataService(_http);
  }

  ngOnInit(): void {
    let param = this._route.snapshot.params['id'];
    this._workCService.endOfUrl = `/employee/${param}`;
    this._workCService.getAll().subscribe(response => {
      this.workConditions.next(response);
    })
    this._employeeDataService.get(param).subscribe(response => {
      this.nameOfEmployee = response.firstName;
    })

  }

  onEdit(workC: WorkConditions) {
    let dateParts = this._updatedDate?.split('/') as string[];
    workC.date = this._updatedDate == null ? workC.date :
      new Date(Number.parseInt(dateParts[0]), Number.parseInt(dateParts[1]), Number.parseInt(dateParts[2]));
    workC.expensesCurrencyId = workC.expensesCurrency.id;
    workC.wageCurrencyId = workC.wageCurrency.id;
    workC.payCurrencyId = workC.payCurrency.id;
    this._workCService.endOfUrl = "";
    this._workCService.update(workC.employeeId as number, workC).subscribe(next => {
    });
  }

  async onDelete(workC: WorkConditions) {
    if (await this._messageService.usersConfirmation()) {
      this._workCService.endOfUrl = "";
      this._workCService.delete((workC as WorkConditions).id as number).subscribe(async response => {
        this.workConditions.next(this.workConditions.value.filter(wc => wc != workC));
      })
    }
  }

  onChange(input: HTMLInputElement) {
    this._updatedDate = input.value;
  }

}
