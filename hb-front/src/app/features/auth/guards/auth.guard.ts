import {CanActivateFn, Router} from "@angular/router";
import {inject} from "@angular/core";
import {AuthState} from "@shared/states";
import {IUser} from "@shared/models";

export const authGuard: CanActivateFn = () => {
  const state: AuthState<IUser> = inject(AuthState);
  const isAuthorized = state.isTokenValid();
  if(!isAuthorized) inject(Router).navigate(["login"]);
  return isAuthorized;
}

export const notAuthGuard: CanActivateFn = () => {
  const state: AuthState<IUser> = inject(AuthState);
  const isNotAuthorized = state.isTokenValid();
  if(!isNotAuthorized) return true;
  inject(Router).navigate(["main"]);
  return false;
}