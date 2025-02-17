import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  firstName: string = 'Enter first name';
  lastName: string = 'Enter last name';
  username: string = '';
  password: string = '';
  repeatPassword: string = '';

  registerForm!: FormGroup;

  constructor(private authService: AuthService) {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(3)]),
      repeatPassword: new FormControl('', [Validators.required, Validators.minLength(3)])
    });
  }

  ngOnInit(): void {
  }

  onRegister() {
    this.authService.register({
      firstName: this.firstName,
      lastName: this.lastName,
      username: this.username,
      password: this.password,
    })
    .subscribe(res => {
      console.log(res);
    });
  }

}
