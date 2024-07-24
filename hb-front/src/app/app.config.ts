import {ApplicationConfig, Injectable} from "@angular/core";
import {provideRouter} from "@angular/router";
import {routes} from "./app.routes";
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {provideAnimationsAsync} from "@angular/platform-browser/animations/async";
import {jwtInterceptor, proxyInterceptor} from "@core/interceptors";
import {DateAdapter, MAT_DATE_LOCALE, NativeDateAdapter, provideNativeDateAdapter} from "@angular/material/core";

@Injectable()
export class AppDateAdapter extends NativeDateAdapter {

  override getFirstDayOfWeek(): number {
    return 1;
  }
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptors([
      proxyInterceptor,
      jwtInterceptor
    ])),
    provideAnimationsAsync(),
    provideNativeDateAdapter(),
    {provide: DateAdapter, useClass: AppDateAdapter},
    {provide: MAT_DATE_LOCALE, useValue: 'ru-RU'}
  ]
};