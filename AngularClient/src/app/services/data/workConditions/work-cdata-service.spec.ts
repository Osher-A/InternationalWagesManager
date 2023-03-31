import { TestBed } from '@angular/core/testing';

import { WorkCDataService } from './work-cdata-service.';

describe('WorkCDataServiceService', () => {
  let service: WorkCDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkCDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
