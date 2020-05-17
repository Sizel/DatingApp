import { MessagesPaginationParams } from './../models/pagination-messages-params';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getMessages(messageParams: MessagesPaginationParams) {
    // return this.http.get<User[]>(this.baseUrl + 'users/ )
  }

}
