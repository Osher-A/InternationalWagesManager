import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkConditionsUpdateComponent } from './update.component';

describe('UpdateComponent', () => {
  let component: WorkConditionsUpdateComponent;
  let fixture: ComponentFixture<WorkConditionsUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WorkConditionsUpdateComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(WorkConditionsUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
