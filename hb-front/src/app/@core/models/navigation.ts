export interface Navigation {
  readonly path: string[];
  readonly fn: () => void;
  readonly icon: string;
  readonly text: string;
}
