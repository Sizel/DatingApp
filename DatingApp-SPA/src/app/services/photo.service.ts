import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  setMainPhoto(id: number, photoId: number) {
    return this.http.put(this.baseUrl + 'users/' + id + '/photos/main/' + photoId, {});
  }

  deletePhoto(id: number, photoId: number) {
    return this.http.delete(this.baseUrl + 'users/' + id + '/photos/' + photoId);
  }
}
