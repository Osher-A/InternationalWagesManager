import { HttpClient } from '@angular/common/http';
import { inject, Injectable, Self } from '@angular/core';
import { WorkConditions } from '../dto/workConditions';
import { DataService } from './data.service';

export class WorkCDataService extends DataService<WorkConditions> {
  constructor(http: HttpClient, public endPoint: string = "") {
    super(http, "https://localhost:7194/api/workconditions" + endPoint);
  }
}
