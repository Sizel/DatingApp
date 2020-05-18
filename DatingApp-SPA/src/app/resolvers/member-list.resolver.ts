import { Observable, of } from 'rxjs';
import { AlertService } from '../services/alert.service';
import { UserService } from '../services/user.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../models/user';
import { catchError } from 'rxjs/operators';
import { PaginationResult } from '../models/pagination';
import { UserPaginationParams } from '../models/pagination-user-params';

@Injectable()
export class MemberListResolver implements Resolve<PaginationResult<User[]>> {
  pageNumber = 1;
  pageSize = 6;

  userPaginationParams: UserPaginationParams = {
    paginationInfo: {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
    },
  };

  constructor(
    private userService: UserService,
    private alertify: AlertService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<PaginationResult<User[]>> {
    return this.userService.getUsers(this.userPaginationParams).pipe(
      catchError((error) => {
        this.alertify.error('Problem retrieving data');
        return of(null);
      })
    );
  }
}
