import { PaginationParams } from 'src/app/models/pagination-params';
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

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsers(paginationParams: PaginationParams): Observable<PaginationResult<User[]>> {
    let params = new HttpParams();
    const paginationResult = new PaginationResult<User[]>();

    params = params.append('pageSize', paginationParams.paginationInfo.pageSize.toString());
    params = params.append('pageNumber', paginationParams.paginationInfo.pageNumber.toString());
    if (paginationParams.gender) {
      params = params.append('gender', paginationParams.gender);
    }
    if (paginationParams.minAge) {
      params = params.append('minAge', paginationParams.minAge.toString());
    }
    if (paginationParams.maxAge) {
      params = params.append('maxAge', paginationParams.maxAge.toString());
    }
    if (paginationParams.orderBy) {
      params = params.append('orderBy', paginationParams.orderBy);
    }
    console.log('request params: ', params);
    return this.http
      .get<User[]>(this.baseUrl + 'users', {
        params,
        observe: 'response',
      })
      .pipe(
        map((response) => {
          const httpResponse = response as HttpResponse<User[]>;
          paginationResult.result = httpResponse.body;
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
}
