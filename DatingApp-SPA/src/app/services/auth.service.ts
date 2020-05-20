import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../models/user';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login(user: User) {
    return this.http.post(this.baseUrl + 'login', user).pipe(
      map((response: any) => {
        if (response) {
          const token = response.token.result;
          localStorage.setItem('token', token);
          localStorage.setItem('user', JSON.stringify(response.user));
          this.decodedToken = this.jwtHelper.decodeToken(token);
        }
      })
    );
  }

  isLoggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem('token');
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  isAuthorized(allowedRoles: Array<string>): boolean {
    const userRoles = this.decodedToken.role as Array<string>;
    for (const role of allowedRoles) {
      if (userRoles.includes(role)) {
        return true;
      }
    }
    return false;
  }
}
