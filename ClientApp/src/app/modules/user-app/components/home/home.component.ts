import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { HomeService } from '../../services/home.service';
import { NewsModalComponent } from '../news-modal/news-modal.component';
import { take } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/modules/auth/services/auth.service';

@UntilDestroy()
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  newsItems: any[] = [];
  personalDetails: any;
  userRole: string = '';

  constructor(
    private homeService: HomeService, 
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private authService: AuthService
  ) {
    this.userRole = authService.userRole;
  }

  ngOnInit(): void {
    this.homeService.getNews()
      .subscribe((news: any) => {
        this.newsItems = news;
      });
    
    this.homeService.getPersonalDetails()
      .subscribe(res => {
        this.personalDetails = res;
      })
  }

  onAddNews() {
    const dialogRef = this.dialog.open(NewsModalComponent, {
      width: '400px',
      data: {},
    });

    dialogRef.afterClosed()
    .pipe(untilDestroyed(this))
    .subscribe(result => {
      if (!result) return;
      this.homeService.addNews(result)
        .subscribe((res: any) => this.newsItems = res);
    });
  }

  onUpdateNews(news: any) {
    const dialogRef = this.dialog.open(NewsModalComponent, {
      width: '400px',
      data: { isEdit: true, newsitem: news },
    });

    dialogRef.afterClosed()
      .pipe(
        take(1),
        untilDestroyed(this)
      )
      .subscribe(result => {
        if (!result) return;
        this.homeService.updateNews(result)
          .subscribe((res: any) => this.newsItems = res);
      });
  }

  onDeleteNews(id: number) {
    this.homeService.deleteNews(id)
      .pipe(
        take(1),
        untilDestroyed(this)
      )
      .subscribe((res: any) => this.newsItems = res);
  }

  onUpdateUserDetails(newPersonalDetails: any) {
    this._snackBar.open('Successfully changed user details', 'close', {
      politeness: 'polite',
      verticalPosition: 'top',
      horizontalPosition: 'right'
    });
  }

}
