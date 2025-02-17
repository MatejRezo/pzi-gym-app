import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkExerciseToPlanModalComponent } from './link-exercise-to-plan-modal.component';

describe('LinkExerciseToPlanModalComponent', () => {
  let component: LinkExerciseToPlanModalComponent;
  let fixture: ComponentFixture<LinkExerciseToPlanModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LinkExerciseToPlanModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LinkExerciseToPlanModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
