export abstract class RouterService {
  public abstract navigate(params: string[]): Promise<boolean>;
}
