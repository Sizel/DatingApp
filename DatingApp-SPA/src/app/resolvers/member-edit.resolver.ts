import { Observable, of } from 'rxjs';
import { AlertService } from '../services/alert.service';
import { UserService } from '../services/user.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../models/user';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

@Injectable()
export class MemberEditResolver implements Resolve<User> {
  constructor(
    private userService: UserService,
    private auth: AuthService,
    private router: Router,
    private alertify: AlertService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<User> {
    return this.userService.getUser(this.auth.decodedToken.nameid).pipe(
      catchError((error) => {
        this.alertify.error('Problem retrieving data');
        this.router.navigate(['/members']);
        return of(null);
      })
    );
  }
}
