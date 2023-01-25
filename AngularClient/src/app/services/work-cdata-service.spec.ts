import { TestBed } from '@angular/core/testing';

import { WorkCDataServiceService } from './work-cdata-service.service';

describe('WorkCDataServiceService', () => {
  let service: WorkCDataServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkCDataServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
