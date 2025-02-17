import jwt_decode from 'jwt-decode';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, take } from 'rxjs';
import { getCurrentEnv } from 'src/app/constants/environments';
import { LocalStorageToken, LoginDetails } from 'src/app/models/types';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = getCurrentEnv();
  locallyStoredData!: LocalStorageToken;
  userRole: any;

  constructor(private http: HttpClient) { }

  getToken(): LocalStorageToken{
    if (!this.locallyStoredData) {
      this.locallyStoredData = JSON.parse(localStorage.getItem('sportiverse-app:auth') as string);
    }

    return this.locallyStoredData || { token: '' };
  }

  setToken(data: LocalStorageToken) {
    localStorage.setItem('sportiverse-app:auth', JSON.stringify(data));
    this.locallyStoredData = data;
  }

  removeToken() {
    localStorage.removeItem('sportiverse-app:auth');
    if(!this.locallyStoredData) return;
    this.locallyStoredData = { token: '', refreshToken: '' };
  }

  getDecodedAccessToken(): any {
    const token = this.getToken().token;
    try {
      this.userRole = (jwt_decode(token) as any)
        ['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      return this.userRole;
    } catch(Error) {
      return null;
    }
  }

  login(userDetails: LoginDetails) {
    const subj = new Subject();

    this.http.post(`${this.apiUrl}/auth/login`, userDetails)
      .pipe(take(1))
      .subscribe((res) => {
        this.setToken(res as LocalStorageToken);
        subj.next(res);
      }, () => {
        // TODO: implement snackbar showing here
        subj.next(true);
      });

    return subj.asObservable();
  }

  logout() {
    this.removeToken();
  }

  register(newUser: any) {
    return this.http.post(`${this.apiUrl}/auth/register`, newUser)
      .pipe(take(1));
  }

  deleteUser(userId: number) {
    return this.http.delete(`${this.apiUrl}/users/${userId}`)
      .pipe(take(1));
  }

  getRoles() {
    return this.http.get(`${this.apiUrl}/auth/roles`)
      .pipe(take(1));
  }
}
