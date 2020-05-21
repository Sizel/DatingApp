import { UserDescription } from './../../../models/user-description';
import { AuthService } from './../../../services/auth.service';
import { UserService } from './../../../services/user.service';
import { AlertService } from './../../../services/alert.service';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/models/user';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css'],
})
export class MemberEditComponent implements OnInit {
  userForEdit: User;
  @ViewChild('editForm', { static: false }) editForm: NgForm;

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertService,
    private authService: AuthService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      if (data) {
        this.userForEdit = data.userForEdit;
      }
    });
  }

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      return ($event.returnValue = true);
    }
  }

  updateUser(form) {
    debugger;
    this.userService
      .updateUser(this.userForEdit, this.userForEdit.id)
      .subscribe(() => {
        this.alertify.success('Changes saved!');
        // Не знаю почему, но после сброса формы то что было в полях описания и интересов удаляется
        // Поэтому я решил их сохранить для последующей перезаписи
        const userDescription = this.userForEdit.userDescription.description;
        const interests = this.userForEdit.userDescription.interests;

        this.editForm.reset(this.userForEdit);

        this.editForm.controls.description.setValue(userDescription);
        this.editForm.controls.interests.setValue(interests);
      }, () => {
        this.alertify.error('Failed to save changes');
      });
  }

  onMainPhotoChange(newMainUrl: string) {
    this.userForEdit.mainPhotoUrl = newMainUrl;
  }
}
