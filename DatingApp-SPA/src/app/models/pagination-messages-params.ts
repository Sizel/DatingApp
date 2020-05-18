import { PaginationParams } from 'src/app/models/pagination-params';

export interface MessagesPaginationParams extends PaginationParams {
  messageType?: 'inbox' | 'outbox' | 'unread';
}
