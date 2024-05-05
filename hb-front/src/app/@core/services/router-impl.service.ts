import {Router} from "@angular/router";
import {RouterService} from "@shared/services/router.service";

export class RouterServiceImpl extends RouterService {
  constructor(private readonly _router: Router, private readonly _prefixParams: string[] = []) {
    super();
  }

  public navigate(params: string[]): Promise<boolean> {
    return this._router.navigate([...this._prefixParams, ...params]);
  }
}
