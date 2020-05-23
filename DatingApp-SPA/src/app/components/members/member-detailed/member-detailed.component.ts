import { AuthService } from 'src/app/services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from './../../../services/alert.service';
import { UserService } from './../../../services/user.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/models/user';
import {
  NgxGalleryOptions,
  NgxGalleryImage,
  NgxGalleryAnimation,
} from 'ngx-gallery-9';
import { NgbNav } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-member-detailed',
  templateUrl: './member-detailed.component.html',
  styleUrls: ['./member-detailed.component.css'],
})
export class MemberDetailedComponent implements OnInit {
  detailedUser: User;
  @ViewChild('nav', { static: true }) navTabs: NgbNav;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertService,
    private userService: UserService,
    public auth: AuthService,
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      if (data) {
        this.detailedUser = data.detailedUser;
        this.galleryImages = this.getImages();
      }
    });

    this.route.queryParams.subscribe((params) => {
      const tabId = params.tab;
      if (tabId) {
        this.navTabs.select(+tabId);
      }
    });

    this.galleryOptions = [
      {
        width: '300px',
        height: '300px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: true,
      },
    ];
  }

  getImages() {
    const images = [];

    for (const photo of this.detailedUser.photos) {
      images.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url,
      });
    }

    return images;
  }

  sendLike() {
    this.userService
      .sendLike(this.auth.decodedToken.nameid, this.detailedUser.id)
      .subscribe(
        () => {
          this.detailedUser.isLiked = true;
          this.alertify.success(
            'You have sent a like to ' + this.detailedUser.username
          );
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  sendDislike() {
    this.userService
      .sendDislike(this.auth.decodedToken.nameid, this.detailedUser.id)
      .subscribe(
        () => {
          this.detailedUser.isLiked = false;
          this.alertify.success(
            'You have sent a dislike to ' + this.detailedUser.username
          );
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }
}
