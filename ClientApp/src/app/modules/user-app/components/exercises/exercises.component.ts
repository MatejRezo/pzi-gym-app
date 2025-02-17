import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { take } from 'rxjs';
import { AuthService } from 'src/app/modules/auth/services/auth.service';
import { ExercisesService } from '../../services/exercises.service';
import { ExercisesModalComponent } from '../exercises-modal/exercises-modal.component';

@Component({
  selector: 'app-exercises',
  templateUrl: './exercises.component.html',
  styleUrls: ['./exercises.component.scss']
})
export class ExercisesComponent implements OnInit {
  exercises: any[] = [];
  userRole: string = '';

  constructor(
    private exercisesService: ExercisesService, 
    private authService: AuthService,
    private dialog: MatDialog
  ) {
    this.userRole = authService.userRole;
  }

  ngOnInit(): void {
    this.exercisesService.getTrainings()
      .subscribe((res: any) => this.exercises = res);
  }

  onDeleteExercise(exerciseId: number) {
    this.exercisesService.deleteTraining(exerciseId)
      .subscribe((res: any) => this.exercises = res);
  }

  onUpdateExercise(exercise: any) {
    const dialogRef = this.dialog.open(ExercisesModalComponent, {
      width: '400px',
      data: { isEdit: true, exerciseItem: exercise },
    });

    dialogRef.afterClosed()
    .pipe(take(1))
    .subscribe(result => {
      if (!result) return;
      this.exercisesService.updateTraining(result)
        .subscribe((res: any) => this.exercises = res);
    });
  }

  onAddExercise() {
    const dialogRef = this.dialog.open(ExercisesModalComponent, {
      width: '400px',
      data: { },
    });

    dialogRef.afterClosed()
    .pipe(take(1))
    .subscribe(result => {
      if (!result) return;
      this.exercisesService.addTraining(result)
        .subscribe((res: any) => this.exercises = res);
    });
  }

}
