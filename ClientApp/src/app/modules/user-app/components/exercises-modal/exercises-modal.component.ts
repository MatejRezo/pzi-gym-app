import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ExercisesService } from '../../services/exercises.service';

@Component({
  selector: 'app-exercises-modal',
  templateUrl: './exercises-modal.component.html',
  styleUrls: ['./exercises-modal.component.scss']
})
export class ExercisesModalComponent implements OnInit {
  exerciseItem: any = {
    name: '',
    description: '',
    id: 0
  };

  allExercises!: any[];

  constructor(
    private exercisesService: ExercisesService,
    public dialogRef: MatDialogRef<ExercisesModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    if (data.exerciseItem?.id) {
      console.log(data)
      this.exerciseItem = { ...data.exerciseItem }
    }
  }

  ngOnInit(): void {
  }

  onCancelClick() {
    this.dialogRef.close();
  }

}
