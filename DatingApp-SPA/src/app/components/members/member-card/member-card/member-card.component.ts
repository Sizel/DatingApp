import { AlertService } from './../../../../services/alert.service';
import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
})
export class MemberCardComponent implements OnInit {
  @Input() user: User;

  constructor(
    private userService: UserService,
    private auth: AuthService,
    private alertify: AlertService
  ) {}

  ngOnInit() {}

  sendLike() {
    this.userService
      .sendLike(this.auth.decodedToken.nameid, this.user.userId)
      .subscribe(
        () => {
          this.alertify.success(
            'You have sent a like to ' + this.user.username.substring(0, 1).toUpperCase() + this.user.username
          );
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
