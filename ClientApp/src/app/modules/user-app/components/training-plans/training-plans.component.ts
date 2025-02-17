import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { switchMap, take } from 'rxjs';
import { AuthService } from 'src/app/modules/auth/services/auth.service';
import { ExercisesService } from '../../services/exercises.service';
import { AddTrainingPlanModalComponent } from '../add-training-plan-modal/add-training-plan-modal.component';
import { LinkExerciseToPlanModalComponent } from '../link-exercise-to-plan-modal/link-exercise-to-plan-modal.component';

@UntilDestroy()
@Component({
  selector: 'app-training-plans',
  templateUrl: './training-plans.component.html',
  styleUrls: ['./training-plans.component.scss']
})
export class TrainingPlansComponent implements OnInit {
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  userRole: string = '';
  trainingPlans: MatTableDataSource<any> = new MatTableDataSource();
  displayedColumns: string[] = ['id', 'name', 'description'];
  selection = new SelectionModel<any>(false, []);
  selectedPlan?: any;
  mappedExercises: any[] = [];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private exercisesService: ExercisesService,
    private dialog: MatDialog,
  ) {
    this.userRole = authService.userRole;
  }

  ngOnInit(): void {
    this.exercisesService.getTrainingPlans()
      .pipe(
        untilDestroyed(this),
        switchMap((plans: any) => this.trainingPlans.data = plans),
      )
      .pipe(
        untilDestroyed(this),
        switchMap(() => this.route.queryParams.pipe(untilDestroyed(this))),
      )
      .subscribe((queryParams: any) => {
        this.selectedPlan = this.trainingPlans.data.find(plan => plan.id === +queryParams.exercisePlan);
        this.selection.toggle(this.selectedPlan);

        if (!this.selectedPlan) return;
        this.mappedExercises = this.selectedPlan.exercises.map((exercise: any) => exercise.exercise);
        console.log();
      });
  }

  ngAfterViewInit(): void {
    this.trainingPlans.sort = this.sort;
    this.trainingPlans.paginator = this.paginator;
  }

  onAddPlan() {
    const dialogRef = this.dialog.open(AddTrainingPlanModalComponent, {
      width: '500px',
      data: {},
    });

    dialogRef.afterClosed()
      .pipe(untilDestroyed(this))
      .subscribe(result => {
        if (!result) return;

        result.exercises = [];
        this.exercisesService.addTrainingPlan(result)
          .subscribe((res: any) => this.trainingPlans.data = res);
      });
  }

  onLinkExercises() {
    const dialogRef =this.dialog.open(LinkExerciseToPlanModalComponent, {
      width: '500px',
      data: { trainingPlanItem: this.selectedPlan },
    });

    dialogRef.afterClosed()
      .pipe(untilDestroyed(this))
      .subscribe((res: any) => {
        if(!res) return;

        this.exercisesService.unlinkAllExercises(this.selectedPlan.id)
          .pipe(take(1))
          .subscribe(() => {
            res.forEach((exercise: any) => {
              this.exercisesService.linkExercisesToPlan(this.selectedPlan.id, exercise.id)
                .pipe(take(1))
                .subscribe((plans: any) => {
                  this.trainingPlans.data = plans;
                  this.router.navigate([], {
                    relativeTo: this.route,
                  });
                });
            });
          });
      })
  }

  applyFilter(e: Event) {
    const filterValue = (e.target as HTMLInputElement).value;
    this.trainingPlans.filter = filterValue.trim().toLowerCase();

    if (this.trainingPlans.paginator) {
      this.trainingPlans.paginator.firstPage();
    }
  }

  onRowSelect(plan: any) {
    this.router.navigate([], {
      queryParams: { exercisePlan: plan.id },
      relativeTo: this.route,
    });
  }


  onDeleteTrainingPlan(id: number) {
    this.exercisesService.deleteTrainingPlan(id)
      .subscribe((res: any) => this.trainingPlans.data = res);
  }

  onDeleteExercise(id: number) {
    
  }

  onUpdateExercise(newExercise: any) {}
}
