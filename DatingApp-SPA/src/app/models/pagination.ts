export interface PaginationInfo {
  totalItems?: number;
  totalPages?: number;
  pageSize: number;
  pageNumber: number;
}

export class PaginationResult<T> {
  result: T;
  paginationInfo: PaginationInfo;
}
