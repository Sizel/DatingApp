import { AlertService } from '../../../services/alert.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css'],
})
export class LoginFormComponent implements OnInit {
  loginModel: any = {};

  constructor(private messageService: MessageService, private auth: AuthService, private alertify: AlertService, private router: Router) {}

  ngOnInit() {}

  login() {
    this.auth.login(this.loginModel).subscribe(
      () => {
        this.router.navigate(['/members']);
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
