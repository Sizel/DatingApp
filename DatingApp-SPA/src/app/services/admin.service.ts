import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsersWithRoles() {
    return this.http.get<User[]>(this.baseUrl + 'admin/usersWithRoles');
  }

  updateRoles(id: number, roles: string[]) {
    return this.http.post(this.baseUrl + 'admin/editRoles/' + id, { roles });
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + 'admin/deleteUser/' + id);
  }
}
