import { PhotoService } from './../../../../services/photo.service';
import { AlertService } from './../../../../services/alert.service';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from './../../../../../environments/environment';
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Photo } from 'src/app/models/photo';
import { FileUploader } from 'ng2-file-upload';

@Component({
  selector: 'app-member-edit-photos',
  templateUrl: './member-edit-photos.component.html',
  styleUrls: ['./member-edit-photos.component.css'],
})
export class MemberEditPhotosComponent implements OnInit {
  @Input() photos: Photo[];
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  @Output() mainPhotoChange = new EventEmitter();

  constructor(
    private auth: AuthService,
    private photoService: PhotoService,
    private alertify: AlertService
  ) {}

  ngOnInit() {
    this.initializeUploader();
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  private initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/' + this.auth.decodedToken.nameid + '/photos',
      isHTML5: true,
      authToken: 'Bearer ' + localStorage.getItem('token'),
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo: Photo = JSON.parse(response);
        this.photos.push(photo);
      }
    };
  }

  setMainPhoto(photoId: number) {
    this.photoService
      .setMainPhoto(this.auth.decodedToken.nameid, photoId)
      .subscribe(
        () => {
          const oldMain = this.photos.find((p) => p.isMain);
          oldMain.isMain = false;
          const newMain = this.photos.find((p) => p.photoId === photoId);
          newMain.isMain = true;
          this.mainPhotoChange.emit(newMain.url);
          this.alertify.success(`You've changed the main photo`);
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  deletePhoto(photoId: number) {
    this.alertify.confirm('Are you sure you want to delete this photo?', () => {
      this.photoService
        .deletePhoto(this.auth.decodedToken.nameid, photoId)
        .subscribe(
          () => {
            this.photos.splice(
              this.photos.findIndex((p) => p.photoId === photoId),
              1
            );
            this.alertify.success('Photo has been deleted');
          },
          (error) => {
            this.alertify.error(error);
          }
        );
    });
  }
}
