import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { pipe, take } from 'rxjs';
import { getCurrentEnv } from 'src/app/constants/environments';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  apiUrl = getCurrentEnv();

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get(`${this.apiUrl}/users`)
      .pipe(take(1));
  }

  updateUser(updatedUser: any) {
    return this.http.patch(`${this.apiUrl}/users`, updatedUser)
      .pipe(take(1));
  }
}
