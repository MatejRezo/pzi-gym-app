import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTrainingPlanModalComponent } from './add-training-plan-modal.component';

describe('AddTrainingPlanModalComponent', () => {
  let component: AddTrainingPlanModalComponent;
  let fixture: ComponentFixture<AddTrainingPlanModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddTrainingPlanModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddTrainingPlanModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
