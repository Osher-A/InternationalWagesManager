import { HttpClient } from '@angular/common/http';
import { WorkConditions } from '../../../dto/workConditions';
import { DataService } from '../data.service';

const URL = "https://localhost:7194/api/workconditions"

export class WorkCDataService extends DataService<WorkConditions> {
  constructor(http: HttpClient) {
    super(http, URL);
  }

  set endOfUrl(value: string) {
    this.url = URL + value;
  }

}
