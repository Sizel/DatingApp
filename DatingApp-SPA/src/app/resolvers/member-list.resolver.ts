import { PaginationParams } from 'src/app/models/pagination-params';
import { Observable, of } from 'rxjs';
import { AlertService } from '../services/alert.service';
import { UserService } from '../services/user.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../models/user';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { PaginationResult } from '../models/pagination';

@Injectable()
export class MemberListResolver implements Resolve<PaginationResult<User[]>> {
  pageNumber = 1;
  pageSize = 6;

  paginationParams: PaginationParams = {
    paginationInfo: {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize
    }
  };

  constructor(private userService: UserService, private auth: AuthService, private router: Router, private alertify: AlertService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<PaginationResult<User[]>> {
    return this.userService.getUsers(this.paginationParams).pipe(
      catchError(error => {
        this.alertify.error('Problem retrieving data');
        return of(null);
      })
    );
  }
}
