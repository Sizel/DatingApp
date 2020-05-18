import { MessagesPaginationParams } from './../../models/pagination-messages-params';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from './../../services/alert.service';
import { MessageService } from './../../services/message.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Message } from 'src/app/models/message';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css'],
})
export class MessagesComponent implements OnInit {
  messages: Message[];
  messagesPaginationParams: MessagesPaginationParams = {
    messageType: 'unread',
    paginationInfo: null,
  };

  constructor(
    private messagesService: MessageService,
    private auth: AuthService,
    private route: ActivatedRoute,
    private alertify: AlertService
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.messages = data.page.result;
      this.messagesPaginationParams.paginationInfo = data.page.paginationInfo;
    });
  }

  changeMessagesType(type: 'unread' | 'inbox' | 'outbox') {
    this.messagesPaginationParams.messageType = type;
    this.loadNextPage();
  }

  loadNextPage() {
    // удали старые сообщения перед загрузкой новых
    // для того что бы старые сообщения не показывались вместо новых до тех пор пока новые не загрузятся
    this.messages = [];
    this.messagesService
      .getMessages(this.auth.decodedToken.nameid, this.messagesPaginationParams)
      .subscribe(
        (page) => {
          this.messages = page.result;
          this.messagesPaginationParams.paginationInfo = page.paginationInfo;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
