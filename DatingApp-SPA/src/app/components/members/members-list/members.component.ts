import { PaginationParams } from './../../../models/pagination-params';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from '../../../services/alert.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../../../models/user';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css'],
})
export class MembersComponent implements OnInit {
  users: User[];
  user: User;
  genders = [{value: 'female', display: 'Females'}, {value: 'male', display: 'Males'}, {value: 'both', display: 'Both'}, ];
  paginationParams: PaginationParams = { paginationInfo: null };

  constructor(
    private userService: UserService,
    private alertify: AlertService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.users = data.page.result;
      this.paginationParams.paginationInfo = data.page.paginationInfo;
    });
  }

  resetFilters() {
    this.paginationParams = {
      paginationInfo: this.paginationParams.paginationInfo
    };
    this.loadNextPage();
    console.log(this.paginationParams);
  }

  applyFilters() {
    this.loadNextPage();
    console.log(this.paginationParams);
  }

  pageChange() {
    this.loadNextPage();
  }

  loadNextPage() {
    this.userService
      .getUsers(this.paginationParams)
      .subscribe(
        (page) => {
          this.users = page.result;
          this.paginationParams.paginationInfo = page.paginationInfo;
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
