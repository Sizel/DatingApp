import { PaginationParams } from 'src/app/models/pagination-params';

export interface UserPaginationParams extends PaginationParams {
  gender?: 'male' | 'female' | 'both';
  userName?: string;
  minAge?: number;
  maxAge?: number;
  orderBy?: string;
  likees?: boolean;
  likers?: boolean;
}
