import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/modules/auth/services/auth.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
})
export class LayoutComponent implements OnInit {
  userRole: string = '';
  menuItems: any[] = [
    {
      name: 'home',
      icon: 'home',
    },
    {
      name: 'training',
      icon: 'fitness_center',
    },
    {
      name: 'about us',
      icon: 'info',
    },
  ];

  constructor(private router: Router, private authService: AuthService) {
    this.authService.getDecodedAccessToken();
    this.userRole = authService.userRole;
    if (this.userRole === 'Owner' || this.userRole === 'Trainer')
      this.menuItems.push({ name: 'users', icon: 'people' });
  }

  ngOnInit(): void {}

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
