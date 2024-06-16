export interface ITableColumn<T extends string = string> {
  readonly id: T;
  readonly order: number;
  readonly text?: string;
  readonly align?: 'center';
  readonly width?: string;
}
