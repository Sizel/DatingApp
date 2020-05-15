import { AlertService } from '../../../services/alert.service';
import { AuthService } from '../../../services/auth.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { User } from 'src/app/models/user';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent implements OnInit {
  @Output() cancel = new EventEmitter();
  regForm: FormGroup;
  user: User;

  constructor(private auth: AuthService, private router: Router, private fb: FormBuilder, private alertify: AlertService) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.regForm = this.fb.group({
      username: ['', Validators.required],
      gender: ['male'],
      dateOfBirth: [null, Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.maxLength(8), Validators.minLength(4)]],
      passwordConfirm: ['', Validators.required]
    }, {validators: this.passwordMatchValidator});
  }

  passwordMatchValidator(group: FormGroup) {
    return group.value.passwordConfirm === group.value.password ? null : { passwordMismatch: true };
  }

  register() {
    if (this.regForm.valid) {
      this.user = Object.assign({}, this.regForm.value);

      this.auth.register(this.user).subscribe(() => {
        this.alertify.success('Registration successful');
        this.auth.login(this.user).subscribe(() => {
          this.router.navigate(['/members']);
        }, error => {
          this.alertify.error(error);
        });
      }, error => {
        this.alertify.error(error);
      });
    }
  }
}
