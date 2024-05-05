import {CanActivateFn} from "@angular/router";
import {inject} from "@angular/core";
import {AuthState} from "@shared/states";
import {map} from "rxjs";
import {IUser} from "@shared/models";

export const authGuard: CanActivateFn = () => {
  const state: AuthState<IUser> = inject(AuthState);
  return state.tokenRaw$.pipe(map((token) => !!token));
}
