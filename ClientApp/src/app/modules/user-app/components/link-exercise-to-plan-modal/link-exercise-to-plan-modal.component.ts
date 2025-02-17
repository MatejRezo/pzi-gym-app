import { SelectionModel } from '@angular/cdk/collections';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ExercisesService } from '../../services/exercises.service';

@Component({
  selector: 'app-link-exercise-to-plan-modal',
  templateUrl: './link-exercise-to-plan-modal.component.html',
  styleUrls: ['./link-exercise-to-plan-modal.component.scss']
})
export class LinkExerciseToPlanModalComponent implements OnInit {
  trainingPlanItem = {
    name: '',
    description: '',
    exercises: [] as any[],
  };

  displayedColumns: string[] = ['select', 'id', 'name'];
  availableExercises = new MatTableDataSource<any>();
  selection = new SelectionModel<any>(true, []);

  constructor(
    private exercisesService: ExercisesService,
    public dialogRef: MatDialogRef<LinkExerciseToPlanModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    if (data.trainingPlanItem?.id) {
      this.trainingPlanItem = data.trainingPlanItem;
    }
  }

  ngOnInit(): void {
    this.exercisesService.getTrainings()
      .subscribe((res: any) => {
        if(res) {
          this.availableExercises.data = res;
          this.selectLinkedExercises(res);
        }
      })
  }

   /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.availableExercises.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.availableExercises.data);
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }


  selectLinkedExercises(exercises: any[]) {
    exercises.forEach((exercise: any) => {
      const exerciseIndex = this.trainingPlanItem.exercises.findIndex(e => e.exercise.id === exercise.id)
      if(exerciseIndex !== -1) {
        this.selection.toggle(exercise);
      }
    });
  }

}
