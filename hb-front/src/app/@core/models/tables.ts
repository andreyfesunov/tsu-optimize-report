export interface ITableColumn<T extends string = string> {
    id: T;
    text: string;
    order: number;
    align?: 'center';
    width?: string;
}
