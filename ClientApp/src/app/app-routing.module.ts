import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginLayoutComponent } from './modules/auth/components/login-layout/login-layout.component';
import { AuthGuard } from './modules/auth/guards/auth.guard';
import { AboutUsComponent } from './modules/user-app/components/about-us/about-us.component';
import { ExercisesComponent } from './modules/user-app/components/exercises/exercises.component';
import { HomeComponent } from './modules/user-app/components/home/home.component';
import { LayoutComponent } from './modules/user-app/components/layout/layout.component';
import { TrainingPlansComponent } from './modules/user-app/components/training-plans/training-plans.component';
import { TrainingComponent } from './modules/user-app/components/training/training.component';
import { UsersComponent } from './modules/user-app/components/users/users.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: LoginLayoutComponent,
  },
  {
    path: 'app',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
      },
      {
        path: 'home',
        component: HomeComponent,
      },
      {
        path: 'users',
        component: UsersComponent,
      },
      {
        path: 'training',
        component: TrainingComponent,
        children: [
          {
            path: '',
            redirectTo: 'plans',
            pathMatch: 'full',
          },
          {
            path: 'plans',
            component: TrainingPlansComponent,
          },
          {
            path: 'exercises',
            component: ExercisesComponent,
          },
        ],
      },
      {
        path: 'about us',
        component: AboutUsComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
