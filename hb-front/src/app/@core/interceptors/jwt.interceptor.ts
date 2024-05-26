import {HttpInterceptorFn} from "@angular/common/http";
import {inject} from "@angular/core";
import {switchMap} from "rxjs";
import {AuthState} from "@core/states";

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const state = inject(AuthState);

  return state.tokenRaw$.pipe(
    switchMap((token) => {
      if (token === undefined) {
        return next(req);
      }

      const clonedReq = req.clone({
        setHeaders: {"Authorization": `Bearer ${token}`}
      });

      return next(clonedReq);
    })
  );
}
