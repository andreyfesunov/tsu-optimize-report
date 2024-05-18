export interface ITableColumn<T extends string = string> {
  readonly id: T;
  readonly text: string;
  readonly order: number;
  readonly align?: 'center';
  readonly width?: string;
}
