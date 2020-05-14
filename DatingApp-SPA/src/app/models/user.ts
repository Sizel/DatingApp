import { Photo } from './photo';
import { UserDescription } from './user-description';

export interface User {
  userId: number;
  username: string;
  age: number;
  gender: string;
  created: Date;
  lastActive: Date;
  mainPhotoUrl: string;
  city: string;
  country: string;
  userDescription?: UserDescription;
  photos?: Photo[];
}
