import { AlertService } from './../services/alert.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  loginModel: any = {};

  constructor(private auth: AuthService, private alertify: AlertService) { }

  ngOnInit() {
  }

  login() {
    this.auth.login(this.loginModel).subscribe(next => {
      this.alertify.success('You are logged in!');
    }, error => {
      this.alertify.error(error);
    });
  }


}
