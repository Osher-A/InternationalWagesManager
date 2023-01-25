import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkConditionsDetailsComponent } from './details.component';

describe('DetailsComponent', () => {
  let component: WorkConditionsDetailsComponent;
  let fixture: ComponentFixture<WorkConditionsDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkConditionsDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkConditionsDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
