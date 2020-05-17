import { PaginationParams } from 'src/app/models/pagination-params';

export interface UserPaginationParams extends PaginationParams {
  gender?: 'male' | 'female' | 'both';
  minAge?: number;
  maxAge?: number;
  orderBy?: string;
  likees?: boolean;
  likers?: boolean;
}
