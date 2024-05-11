export interface IPagination<T> {
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    entities: T[];
}
