import { MessageToSend } from './../models/messageToSend';
import { map } from 'rxjs/operators';
import { MessagesPaginationParams } from './../models/pagination-messages-params';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { PaginationResult } from '../models/pagination';
import { Observable } from 'rxjs';
import { Message } from '../models/message';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  sendMessage(message: MessageToSend, userId: number) {
    return this.http.post(this.baseUrl + 'users/' + userId + '/messages', message);
  }

  getConversation(userId: number, recipientId: number) {
    return this.http.get<Message[]>(
      this.baseUrl + 'users/' + userId + '/messages/conv/' + recipientId
    );
  }
  getMessages(
    userId: number,
    messageParams: MessagesPaginationParams
  ): Observable<PaginationResult<Message[]>> {
    let params = new HttpParams();

    params = params.append(
      'pageSize',
      messageParams.paginationInfo.pageSize.toString()
    );
    params = params.append(
      'pageNumber',
      messageParams.paginationInfo.pageNumber.toString()
    );

    if (messageParams.messageType) {
      params = params.append('messageType', messageParams.messageType);
    }
    console.log('message request params: ', params);

    return this.http
      .get<Message[]>(this.baseUrl + 'users/' + userId + '/messages/', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          const paginationResult = new PaginationResult<Message[]>();
          paginationResult.result = response.body;
          paginationResult.paginationInfo = JSON.parse(
            response.headers.get('Pagination')
          );
          return paginationResult;
        })
      );
  }
}
