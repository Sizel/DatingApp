import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login(user: any) {
    return this.http.post(this.baseUrl + 'login', user).pipe(
      map((response: any) => {
        if (response) {
          localStorage.setItem('token', response.result);
          this.decodedToken = this.jwtHelper.decodeToken(response.result);
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

  register(user: any) {
    return this.http.post(this.baseUrl + 'register', user);
  }
}
