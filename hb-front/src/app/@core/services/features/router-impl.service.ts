import {Router} from "@angular/router";
import {Injectable} from "@angular/core";
import {RouterService} from "@core/abstracts";

@Injectable()
export class RouterServiceImpl extends RouterService {
  constructor(private readonly _router: Router) {
    super();
  }

  /** For now just wrapper */
  public navigate(params: string[]): Promise<boolean> {
    return this._router.navigate(params);
  }
}
