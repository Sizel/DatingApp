import { Photo } from './../../../models/photo';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from './../../../services/alert.service';
import { UserService } from './../../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery-9';

@Component({
  selector: 'app-member-detailed',
  templateUrl: './member-detailed.component.html',
  styleUrls: ['./member-detailed.component.css']
})
export class MemberDetailedComponent implements OnInit {
  user: User;

  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private userService: UserService, private alertify: AlertService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(user => {
      if (user) {
        this.user = user.user;
        this.galleryImages = this.getImages();
      }
    });

    this.galleryOptions = [
      {
        width: '300px',
        height: '300px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: true
      }
    ];

  }

  getImages() {
    const images = [];

    for (const photo of this.user.photos) {
      images.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url,
        description: photo.description
      });
    }

    return images;
  }
}
