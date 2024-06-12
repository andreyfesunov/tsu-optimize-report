export interface IComment {
  readonly id: string;
  readonly content: string;
  readonly factDate: number | null;
  readonly planDate: number | null;
}
