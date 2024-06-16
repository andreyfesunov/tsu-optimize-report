import {CanActivateFn} from "@angular/router";
import {AuthState} from "@core/states";
import {inject} from "@angular/core";
import {map} from "rxjs";
import {RoleEnum} from "@core/models";

export const roleFn$ = (role: RoleEnum) => {
  const state: AuthState = inject(AuthState);

  return state.user$.pipe(map((user) => Number(user.role) >= role))
}

export const roleGuard: (role: RoleEnum) => CanActivateFn = (role: RoleEnum) => {
  return () => roleFn$(role)
}
