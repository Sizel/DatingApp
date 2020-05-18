import { Observable, of } from 'rxjs';
import { AlertService } from './../services/alert.service';
import { UserService } from './../services/user.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../models/user';
import { catchError } from 'rxjs/operators';

@Injectable()
export class MemberDetailResolver implements Resolve<User> {
  constructor(
    private userService: UserService,
    private router: Router,
    private alertify: AlertService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<User> {
    return this.userService.getUser(route.params.id).pipe(
      catchError((error) => {
        this.alertify.error('Problem retrieving data');
        this.router.navigate(['/members']);
        return of(null);
      })
    );
  }
}
