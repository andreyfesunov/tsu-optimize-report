export interface IPagination<T> {
  readonly pageNumber: number;
  readonly pageSize: number;
  readonly totalPages: number;
  readonly entities: T[];
}
