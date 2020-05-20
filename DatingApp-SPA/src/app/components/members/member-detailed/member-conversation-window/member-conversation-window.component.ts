import { NgForm } from '@angular/forms';
import { AuthService } from './../../../../services/auth.service';
import { Component, OnInit, Input } from '@angular/core';
import { AlertService } from 'src/app/services/alert.service';
import { UserService } from 'src/app/services/user.service';
import { MessageService } from 'src/app/services/message.service';
import { User } from 'src/app/models/user';
import { Message } from 'src/app/models/message';
import { MessageToSend } from 'src/app/models/messageToSend';

@Component({
  selector: 'app-member-conversation-window',
  templateUrl: './member-conversation-window.component.html',
  styleUrls: ['./member-conversation-window.component.css']
})
export class MemberConversationWindowComponent implements OnInit {
  @Input() user: User;
  messages: Message[];
  messageToSend: MessageToSend = {
    content: '',
    recipientId: 0
  };

  constructor(private messagesService: MessageService, private auth: AuthService, private alertify: AlertService) { }

  ngOnInit() {
    this.loadMessages();
  }

  loadMessages() {
    this.messagesService.getConversation(this.auth.decodedToken.nameid, this.user.id).subscribe(messages => {
      this.messages = messages;
    }, error => {
      this.alertify.error(error);
    });
  }

  sendMessage() {
    this.messageToSend.recipientId = this.user.id;
    this.messagesService.sendMessage(this.messageToSend, this.auth.decodedToken.nameid).subscribe((message: Message) => {
      console.log(message);
      this.messages.unshift(message);
    }, () => {
      this.alertify.error('Problem sending message to db');
    });
    this.messageToSend.content = '';
  }

}
