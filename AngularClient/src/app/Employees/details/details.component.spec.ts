import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeesDetailsComponent } from './details.component';

describe('DetailsComponent', () => {
  let component: EmployeesDetailsComponent;
  let fixture: ComponentFixture<EmployeesDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeesDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeesDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
