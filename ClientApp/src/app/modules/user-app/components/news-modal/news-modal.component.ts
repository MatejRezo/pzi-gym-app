import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-news-modal',
  templateUrl: './news-modal.component.html',
  styleUrls: ['./news-modal.component.scss']
})
export class NewsModalComponent implements OnInit {
  newsItem: any = {
    title: '',
    description: '',
    id: 0
  }

  constructor(
    public dialogRef: MatDialogRef<NewsModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    if (data.id) {
      this.newsItem.id = data.newsitem.id
    }
  }

  ngOnInit(): void {
  }

  onCancelClick() {
    this.dialogRef.close();
  }

}
