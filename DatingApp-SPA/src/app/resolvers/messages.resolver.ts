import { AuthService } from './../services/auth.service';
import { MessagesPaginationParams } from './../models/pagination-messages-params';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { PaginationResult } from '../models/pagination';
import { AlertService } from '../services/alert.service';
import { Observable, of } from 'rxjs';
import { User } from '../models/user';
import { catchError } from 'rxjs/operators';
import { MessageService } from '../services/message.service';
import { Message } from '../models/message';

@Injectable()
export class MessagesResolver implements Resolve<PaginationResult<Message[]>> {
  pageNumber = 1;
  pageSize = 6;

  messagesPaginationParams: MessagesPaginationParams = {
    paginationInfo: {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
    },
    messageType: 'unread'
  };

  constructor(
    private messagesService: MessageService,
    private auth: AuthService,
    private alertify: AlertService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<PaginationResult<Message[]>> {
    return this.messagesService.getMessages(this.auth.decodedToken.nameid, this.messagesPaginationParams).pipe(
      catchError((error) => {
        this.alertify.error('Problem retrieving data');
        return of(null);
      })
    );
  }
}
