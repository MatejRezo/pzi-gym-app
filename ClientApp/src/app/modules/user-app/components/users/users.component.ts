import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../../services/users.service';
import { take, switchMap } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { RegisterComponent } from 'src/app/modules/auth/components/register/register.component';

@UntilDestroy()
@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  users: MatTableDataSource<any> = new MatTableDataSource();
  selectedUser?: any;
  selection = new SelectionModel<any>(false, []);
  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'userRole'];

  constructor(
    private usersService: UsersService,
    private router: Router,
    private route: ActivatedRoute,
    private _snackBar: MatSnackBar,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.usersService.getUsers()
      .pipe(
        untilDestroyed(this),
        switchMap(res => this.users.data = res as any[]),
      )
      .pipe(
        untilDestroyed(this),
        take(1),
        switchMap(() => this.route.queryParams.pipe(untilDestroyed(this))),
      )
      .subscribe((queryParams: any) => {
        this.selectedUser = this.users.data.find(emp => emp.id === +queryParams.employee);
        this.selection.toggle(this.selectedUser);
      });
  }

  ngAfterViewInit(): void {
    this.users.sort = this.sort;
    this.users.paginator = this.paginator;
  }

  applyFilter(e: Event) {
    const filterValue = (e.target as HTMLInputElement).value;
    this.users.filter = filterValue.trim().toLowerCase();

    if (this.users.paginator) {
      this.users.paginator.firstPage();
    }
  }

  onRowSelect(row: any) {
    this.router.navigate([], {
      queryParams: { employee: row.id },
      relativeTo: this.route,
    });
  }

  onUserDetailsUpdate(newList: any) {
    this.users.data = newList;
    this._snackBar.open('Successfully changed user details', 'close', {
      politeness: 'polite',
      verticalPosition: 'top',
      horizontalPosition: 'right'
    });
  }

  onAddUser() {
    const dialogRef = this.dialog.open(RegisterComponent, {
      width: '500px',
      data: {},
    });

    dialogRef.afterClosed()
    .pipe(untilDestroyed(this))
    .subscribe(() => {
      this.usersService.getUsers()
        .pipe(take(1))
        .subscribe((res: any) => this.users.data = res);
    });
  }

}

