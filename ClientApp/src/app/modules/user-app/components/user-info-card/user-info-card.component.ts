import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/modules/auth/services/auth.service';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-user-info-card',
  templateUrl: './user-info-card.component.html',
  styleUrls: ['./user-info-card.component.scss']
})
export class UserInfoCardComponent implements OnInit, OnChanges {
  @Input() personalDetails: any;
  @Output() updatedUsers: EventEmitter<any> = new EventEmitter<any>();
  newPersonalDetails: any = {
    firstName: '',
    lastName: '',
  };
  userInfoFrom!: FormGroup;
  roles: any[] = [];
  userRole: string = '';

  checkIfDataChanged = () => {
    if(!this.personalDetails) return false;

    return JSON.stringify({
      firstName: this.personalDetails.firstName,
      lastName: this.personalDetails.lastName,
    }) === JSON.stringify(this.newPersonalDetails);
  };

  constructor(private usersService: UsersService, private authService: AuthService) {
    this.userInfoFrom = new FormGroup({
      firstname: new FormControl('', Validators.required),
      lastname: new FormControl('', Validators.required),
      selectedRole: new FormControl(),
    });
  }

  ngOnInit(): void {
    this.authService.getRoles()
      .subscribe((res: any) => this.roles = res);
    this.authService.getDecodedAccessToken();
    this.userRole = this.authService.userRole;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.personalDetails) {
      Object.assign(this.newPersonalDetails, {
        firstName: this.personalDetails.firstName,
        lastName: this.personalDetails.lastName,
      });
    }
  }

  onUserInfoUpdate(updatedUser: any) {
    Object.assign(this.personalDetails, updatedUser);

    this.usersService.updateUser(this.personalDetails)
      .subscribe(res => {
        this.updatedUsers.emit(res);
      });
  }

  onCancleUpdate() {
    if(this.personalDetails) {
      Object.assign(this.newPersonalDetails, {
        firstName: this.personalDetails.firstName,
        lastName: this.personalDetails.lastName,
      });
    }
  }

  onDeleteUser() {
    this.authService.deleteUser(this.personalDetails.id)
      .subscribe(res => this.updatedUsers.emit(res));
  }

}
