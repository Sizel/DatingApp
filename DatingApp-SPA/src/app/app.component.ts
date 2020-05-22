import { AlertService } from './services/alert.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { MessageService } from './services/message.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  constructor(
    private auth: AuthService,
    private messageService: MessageService,
    private alertify: AlertService
  ) {}

  ngOnInit(): void {
    const token = localStorage.getItem('token');

    if (token) {
      this.auth.decodedToken = new JwtHelperService().decodeToken(token);
    }

    this.messageService.connection.on('NewMessageNotification', () => {
      this.alertify.success('You have new message');
    });
  }
}
