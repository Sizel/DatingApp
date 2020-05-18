import { PaginationParams } from './../../../models/pagination-params';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from '../../../services/alert.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../../../models/user';
import { UserService } from '../../../services/user.service';
import { UserPaginationParams } from 'src/app/models/pagination-user-params';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css'],
})
export class MembersComponent implements OnInit {
  users: User[];
  genders = [{value: 'female', display: 'Females'}, {value: 'male', display: 'Males'}, {value: 'both', display: 'Both'}, ];
  userPaginationParams: UserPaginationParams = { paginationInfo: null };

  constructor(
    private userService: UserService,
    private alertify: AlertService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.users = data.page.result;
      this.userPaginationParams.paginationInfo = data.page.paginationInfo;
    });
  }

  resetFilters() {
    this.userPaginationParams = {
      paginationInfo: this.userPaginationParams.paginationInfo
    };
    this.loadNextPage();
  }

  loadNextPage() {
    this.userService
      .getUsers(this.userPaginationParams)
      .subscribe(
        (page) => {
          this.users = page.result;
          this.userPaginationParams.paginationInfo = page.paginationInfo;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
