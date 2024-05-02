import {CanActivateFn} from "@angular/router";
import {inject} from "@angular/core";
import {AuthState} from "@shared/states";
import {map} from "rxjs";

export const authGuard: CanActivateFn = () => {
  const state = inject(AuthState);
  return state.tokenRaw$.pipe(map((token) => !!token));
}
