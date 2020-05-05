import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';

  constructor(private http: HttpClient) { }

  login(user: any) {
    return this.http.post(this.baseUrl + 'login', user).pipe(
      map((response: any) => {
        if (response) {
          localStorage.setItem('token', response.result);
        }
      })
    );
  }

  isLoggedIn() {
    return !!localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
  }

  register(user: any) {
    return this.http.post(this.baseUrl + 'register', user);
  }
}
