import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExercisesModalComponent } from './exercises-modal.component';

describe('ExercisesModalComponent', () => {
  let component: ExercisesModalComponent;
  let fixture: ComponentFixture<ExercisesModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExercisesModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExercisesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
