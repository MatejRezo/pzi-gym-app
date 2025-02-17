import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../modules/auth/services/auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public auth: AuthService) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.auth.getToken().token || request.url.includes('login')) return next.handle(request);

    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.auth.getToken().token}`
      }
    });
    return next.handle(request);
  }
}