import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeesIndexComponent } from './list.component';

describe('ListComponent', () => {
  let component: EmployeesIndexComponent;
  let fixture: ComponentFixture<EmployeesIndexComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeesIndexComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeesIndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
