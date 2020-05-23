import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { PaginationParams } from 'src/app/models/pagination-params';
import { UserService } from 'src/app/services/user.service';
import { AlertService } from 'src/app/services/alert.service';
import { ActivatedRoute } from '@angular/router';
import { UserPaginationParams } from 'src/app/models/pagination-user-params';

@Component({
  selector: 'app-likes',
  templateUrl: './likes.component.html',
  styleUrls: ['./likes.component.css'],
})
export class LikesComponent implements OnInit {
  users: User[];
  genders = [
    { value: 'female', display: 'Females' },
    { value: 'male', display: 'Males' },
    { value: 'both', display: 'Both' },
  ];
  userPaginationParams: UserPaginationParams = {
    paginationInfo: null,
    likees: true,
    orderBy: 'ageAsc'
  };

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
      paginationInfo: this.userPaginationParams.paginationInfo,
      likees: this.userPaginationParams.likees,
      likers: this.userPaginationParams.likers,
    };
    this.loadNextPage();
  }

  setLikeParams(show: 'likers' | 'likees') {
    if (show === 'likers') {
      this.userPaginationParams.likers = true;
      this.userPaginationParams.likees = false;
    } else if (show === 'likees') {
      this.userPaginationParams.likers = false;
      this.userPaginationParams.likees = true;
    }
    this.loadNextPage();
  }

  loadNextPage() {
    this.userService.getUsers(this.userPaginationParams).subscribe(
      (page) => {
        this.users = page.result;
        this.userPaginationParams.paginationInfo = page.paginationInfo;
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  onDislike(user: User) {
    const index = this.users.indexOf(user);
    this.users.splice(index, 1 );
  }
}
