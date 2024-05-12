export interface Navigation {
  path: string[];
  fn: () => void;
  icon: string;
  text: string;
}
