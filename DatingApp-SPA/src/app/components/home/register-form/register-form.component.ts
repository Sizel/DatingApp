import { AlertService } from '../../../services/alert.service';
import { AuthService } from '../../../services/auth.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent implements OnInit {
  @Output() cancel = new EventEmitter();

  registerModel: any = {};

  constructor(private auth: AuthService, private alertify: AlertService) { }

  ngOnInit() {
  }

  register() {
    this.auth.register(this.registerModel).subscribe(() => {
    this.alertify.success('You are successfuly registered');
    }, error => {
      this.alertify.error(error);
    });
  }

}
