import { Photo } from './photo';
import { UserDescription } from './user-description';

export interface User {
  id: number;
  username: string;
  age: number;
  gender: string;
  created: Date;
  lastActive: Date;
  mainPhotoUrl: string;
  city: string;
  country: string;
  roles: string[];
  userDescription?: UserDescription;
  photos?: Photo[];
  isLiked?: boolean;
}
