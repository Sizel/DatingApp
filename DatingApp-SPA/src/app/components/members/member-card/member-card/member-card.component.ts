import { AlertService } from './../../../../services/alert.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
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
  @Output() dislike = new EventEmitter();
  constructor(
    private userService: UserService,
    private auth: AuthService,
    private alertify: AlertService
  ) {}

  ngOnInit() {}

  sendLike() {
    this.userService
      .sendLike(this.auth.decodedToken.nameid, this.user.id)
      .subscribe(
        () => {
          this.user.isLiked = true;
          this.alertify.success(
            'You have sent a like to ' + this.user.username
          );
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  sendDislike() {
    this.userService
      .sendDislike(this.auth.decodedToken.nameid, this.user.id)
      .subscribe(
        () => {
          this.dislike.emit(this.user);
          this.user.isLiked = false;
          this.alertify.success(
            'You have sent a dislike to ' + this.user.username
          );
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
