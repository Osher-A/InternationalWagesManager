import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkConditionsIndexComponent } from './index.component';

describe('IndexComponent', () => {
  let component: WorkConditionsIndexComponent;
  let fixture: ComponentFixture<WorkConditionsIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkConditionsIndexComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkConditionsIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
