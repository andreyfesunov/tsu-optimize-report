import {CanActivateFn} from "@angular/router";
import {inject} from "@angular/core";
import {toLogin, toMain} from "@core/models";
import {map, tap} from "rxjs";
import {RouterService} from "@core/services";
import {AuthState} from "@core/states";

export const authGuard: CanActivateFn = () => {
  const state: AuthState = inject(AuthState);
  const router: RouterService = inject(RouterService);

  return state.valid$.pipe(
    tap((valid) => !valid && router.navigate(toLogin))
  );
}

export const notAuthGuard: CanActivateFn = () => {
  const state: AuthState = inject(AuthState);
  const router: RouterService = inject(RouterService);

  return state.valid$.pipe(
    tap((valid) => valid && router.navigate(toMain)),
    map((v) => !v)
  )
}
