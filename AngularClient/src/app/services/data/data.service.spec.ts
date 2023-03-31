import { TestBed } from '@angular/core/testing';
import { Employee } from '../../dto/employee';

import { DataService } from './data.service';

describe('EmployeeDataService', () => {
  let service: DataService<Employee>;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
