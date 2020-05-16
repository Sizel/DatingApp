import { PaginationInfo } from './pagination';
export interface PaginationParams {
  paginationInfo: PaginationInfo;
  gender?: string;
  minAge?: number;
  maxAge?: number;
  orderBy?: string;
}
