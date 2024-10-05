import {Router} from "@angular/router";
import {Injectable, inject} from "@angular/core";

@Injectable({providedIn: "root"})
export class RouterService {
  private readonly _router = inject(Router);

  /** For now just wrapper */
  public navigate(params: string[]): Promise<boolean> {
    return this._router.navigate(params);
  }
}
