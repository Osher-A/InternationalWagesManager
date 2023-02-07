import { HttpClient } from '@angular/common/http';
import { inject, Injectable, Self } from '@angular/core';
import { WorkConditions } from '../dto/workConditions';
import { DataService } from './data.service';

const URL = "https://localhost:7194/api/workconditions"

export class WorkCDataService extends DataService<WorkConditions> {
  public endPoint2: string = "";
  constructor(http: HttpClient, public endPoint: string = "") {
    super(http, URL + endPoint);
  }

  set endOfUrl(value: string) {
    this.url = URL + value;
  }

}
