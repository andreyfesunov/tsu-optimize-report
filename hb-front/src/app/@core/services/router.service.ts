import {Router} from "@angular/router";
import {Injectable} from "@angular/core";

@Injectable({providedIn: 'root'})
export class RouterService {
  constructor(private readonly _router: Router) {
  }

  public navigate(params: string[]): Promise<boolean> {
    return this._router.navigate(params);
  }
}
