import {FactoryProvider, Provider} from "@angular/core";
import {AUTH_ROUTER} from "@shared/tokens/routers";
import {Router} from "@angular/router";
import {authRouterFactory} from "@core/factories/routers";

const authRouterProvider: FactoryProvider = {
  provide: AUTH_ROUTER,
  useFactory: authRouterFactory,
  deps: [Router],
}

export const routersProviders: Provider[] = [
  authRouterProvider
];

