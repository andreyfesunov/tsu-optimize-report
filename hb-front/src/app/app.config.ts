import {ApplicationConfig} from "@angular/core";
import {provideRouter} from "@angular/router";
import {routes} from "./app.routes";
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {provideAnimationsAsync} from "@angular/platform-browser/animations/async";
import {jwtInterceptor, proxyInterceptor} from "@core/interceptors";
import {
  authProviders,
  jobsProviders,
  reportsProviders,
  routersProviders,
  statesProviders,
  usersProviders
} from "@core/providers";
import {provideNativeDateAdapter} from "@angular/material/core";
import {institutesProviders} from "@core/providers/institutes";

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([
      proxyInterceptor,
      jwtInterceptor
    ])),
    provideAnimationsAsync(),
    provideNativeDateAdapter(),

    ...routersProviders,
    ...authProviders,
    ...reportsProviders,
    ...usersProviders,
    ...statesProviders,
    ...jobsProviders,
    ...institutesProviders
  ]
};
