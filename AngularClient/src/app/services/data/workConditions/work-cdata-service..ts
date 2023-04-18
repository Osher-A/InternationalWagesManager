import { HttpClient } from '@angular/common/http';
import { WorkConditions } from '../../../dto/workConditions';
import { DataService } from '../data.service';

export class WorkCDataService extends DataService<WorkConditions> {
  constructor(http: HttpClient) {
    super(http, "/workconditions");
  }
 
  set endOfUrl(value: string) {
    this.url += value;
  }

}
