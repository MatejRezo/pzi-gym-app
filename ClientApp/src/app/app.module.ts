import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Angular material components
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatRippleModule } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';

// My components
import { LoginComponent } from './modules/auth/components/login/login.component';
import { ExercisesComponent } from './modules/user-app/components/exercises/exercises.component';
import { HomeComponent } from './modules/user-app/components/home/home.component';
import { LayoutComponent } from './modules/user-app/components/layout/layout.component';
import { UsersComponent } from './modules/user-app/components/users/users.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegisterComponent } from './modules/auth/components/register/register.component';
import { LoginLayoutComponent } from './modules/auth/components/login-layout/login-layout.component';
import { TrainingComponent } from './modules/user-app/components/training/training.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { NewsModalComponent } from './modules/user-app/components/news-modal/news-modal.component';
import { UserInfoCardComponent } from './modules/user-app/components/user-info-card/user-info-card.component';
import { ExercisesModalComponent } from './modules/user-app/components/exercises-modal/exercises-modal.component';
import { AboutUsComponent } from './modules/user-app/components/about-us/about-us.component';
import { TrainingPlansComponent } from './modules/user-app/components/training-plans/training-plans.component';
import { AddTrainingPlanModalComponent } from './modules/user-app/components/add-training-plan-modal/add-training-plan-modal.component';
import { LinkExerciseToPlanModalComponent } from './modules/user-app/components/link-exercise-to-plan-modal/link-exercise-to-plan-modal.component';

// My services
import { AuthService } from './modules/auth/services/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    UsersComponent,
    ExercisesComponent,
    LayoutComponent,
    LoginLayoutComponent,
    TrainingComponent,
    NewsModalComponent,
    UserInfoCardComponent,
    ExercisesModalComponent,
    AboutUsComponent,
    TrainingPlansComponent,
    AddTrainingPlanModalComponent,
    LinkExerciseToPlanModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    // angular material
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatTabsModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatDialogModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatSnackBarModule,
    MatSelectModule,
    MatRippleModule,
    MatCheckboxModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    AuthService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
