import {CanActivateFn} from "@angular/router";
import {inject} from "@angular/core";
import {ITokenModel, toLogin, toMain} from "@core/models";
import {AuthState, RouterService} from "@core/abstracts";
import {map, tap} from "rxjs";

export const authGuard: CanActivateFn = () => {
    const state: AuthState<ITokenModel> = inject(AuthState);
    const router: RouterService = inject(RouterService);

    return state.valid$.pipe(
        tap((valid) => !valid && router.navigate(toLogin))
    );
}

export const notAuthGuard: CanActivateFn = () => {
    const state: AuthState<ITokenModel> = inject(AuthState);
    const router: RouterService = inject(RouterService);

    return state.valid$.pipe(
        tap((valid) => valid && router.navigate(toMain)),
        map((v) => !v)
    )
}
