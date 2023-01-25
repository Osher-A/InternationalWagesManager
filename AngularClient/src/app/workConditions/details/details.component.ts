import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { WorkConditions } from '../../dto/workConditions';
import { WorkCDataService } from '../../services/work-cdata-service.';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class WorkConditionsDetailsComponent implements OnInit {

  workConditions: WorkConditions[] = [];
  _workCService: WorkCDataService | undefined;

  constructor(private _http: HttpClient, private _route: ActivatedRoute) {
  }
  ngOnInit(): void {
    let param = this._route.snapshot.params['id'];
    this._workCService = new WorkCDataService(this._http, `/employee/${param}`);
    this._workCService.getAll().subscribe(response => {
      this.workConditions = response;
      console.log(this.workConditions);
    })

  }

  onEdit(workC: WorkConditions) {

  }

  onDelete(workC: WorkConditions) {

  }
}
