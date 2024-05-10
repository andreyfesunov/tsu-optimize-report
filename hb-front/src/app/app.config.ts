import {ApplicationConfig} from "@angular/core";
import {provideRouter} from "@angular/router";
import {routes} from "./app.routes";
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {provideAnimationsAsync} from "@angular/platform-browser/animations/async";
import {jwtInterceptor, proxyInterceptor} from "@core/interceptors";
import {authProviders, routersProviders} from "@core/providers";

export const appConfig: ApplicationConfig = {
    providers: [
        provideRouter(routes),
        provideHttpClient(withInterceptors([
            proxyInterceptor,
            jwtInterceptor
        ])),
        provideAnimationsAsync(),

        ...routersProviders,
        ...authProviders
    ]
};
