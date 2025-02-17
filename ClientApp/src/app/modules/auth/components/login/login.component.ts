import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  username: string = 'string';
  password: string = 'string';

  loginForm!: FormGroup;

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup(
      {
      username : new FormControl('',[Validators.required]),
      password : new FormControl('', [Validators.required, Validators.minLength(3)])
      }
    )
  }

  onLogin() {
    this.authService
      .login({
        username: this.username,
        password: this.password,
      })
      .subscribe(() => this.router.navigate(['/app']));
  }

}
