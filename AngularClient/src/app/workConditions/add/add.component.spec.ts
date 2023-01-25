import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkConditionsAddComponent } from './add.component';

describe('AddComponent', () => {
  let component: WorkConditionsAddComponent;
  let fixture: ComponentFixture<WorkConditionsAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkConditionsAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkConditionsAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
