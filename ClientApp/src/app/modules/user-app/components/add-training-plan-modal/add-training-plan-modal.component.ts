import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-training-plan-modal',
  templateUrl: './add-training-plan-modal.component.html',
  styleUrls: ['./add-training-plan-modal.component.scss']
})
export class AddTrainingPlanModalComponent implements OnInit {
  trainingPlanItem: any = {
    name: '',
    description: '',
    id: 0
  }

  constructor(
    public dialogRef: MatDialogRef<AddTrainingPlanModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    if (data.trainingPlanItem?.id) {
      this.trainingPlanItem = { ...data.trainingPlanItem }
    }
  }

  ngOnInit(): void {
  }

  onCancelClick() {
    this.dialogRef.close();
  }

}
