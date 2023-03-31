import { TestBed } from '@angular/core/testing';

import { SalaryDataService } from './salary.service';

describe('SalaryService', () => {
  let service: SalaryDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SalaryDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
