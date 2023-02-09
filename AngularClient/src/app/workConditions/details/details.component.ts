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
  styleUrls: ['../../app.component.css'],
})
export class WorkConditionsDetailsComponent implements OnInit {
  private _workCService: WorkCDataService;
  private _updatedDate: string | null = null;
  workConditions = new BehaviorSubject<WorkConditions[]>([]);
  nameOfEmployee: string = "";

  constructor(private _http: HttpClient, private _router: Router, private _route: ActivatedRoute, private _employeeDataService: EmployeeDataService,
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
    this._router.navigate([`workconditions/update/${workC.id}`])
  }

  async onDelete(workC: WorkConditions) {
    if (await this._messageService.usersConfirmation()) {
      this._workCService.endOfUrl = "";
      this._workCService.delete((workC as WorkConditions).id as number).subscribe(async response => {
        this.workConditions.next(this.workConditions.value.filter(wc => wc != workC));
      })
    }
  }
}
