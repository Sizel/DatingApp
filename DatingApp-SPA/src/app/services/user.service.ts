import { PaginationResult } from './../models/pagination';
import { Observable } from 'rxjs';
import {
  HttpClient,
  HttpParams,
  HttpResponse,
} from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { map } from 'rxjs/operators';
import { UserPaginationParams } from '../models/pagination-user-params';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsers(userPaginationParams: UserPaginationParams): Observable<PaginationResult<User[]>> {
    let params = new HttpParams();

    params = params.append('pageSize', userPaginationParams.paginationInfo.pageSize.toString());
    params = params.append('pageNumber', userPaginationParams.paginationInfo.pageNumber.toString());

    if (userPaginationParams.gender) {
      params = params.append('gender', userPaginationParams.gender);
    }
    if (userPaginationParams.minAge) {
      params = params.append('minAge', userPaginationParams.minAge.toString());
    }
    if (userPaginationParams.maxAge) {
      params = params.append('maxAge', userPaginationParams.maxAge.toString());
    }
    if (userPaginationParams.orderBy) {
      params = params.append('orderBy', userPaginationParams.orderBy);
    }
    if (userPaginationParams.likees) {
      params = params.append('likees', 'true');
    }
    if (userPaginationParams.likers) {
      params = params.append('likers', 'true');
    }

    console.log('request params: ', params);
    return this.http
      .get<User[]>(this.baseUrl + 'users', {
        params,
        observe: 'response',
      })
      .pipe(
        map((response) => {
          const paginationResult = new PaginationResult<User[]>();
          paginationResult.result = response.body;
          paginationResult.paginationInfo = JSON.parse(
            response.headers.get('Pagination')
          );
          return paginationResult;
        })
      );
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  updateUser(userForUpdate: User, id: number) {
    return this.http.put(this.baseUrl + 'users/' + id, userForUpdate);
  }

  sendLike(id: number, recipientId: number) {
    return this.http.post(this.baseUrl + 'users/' + id + '/like/' + recipientId, {});
  }
}
