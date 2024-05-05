import {Provider} from "@angular/core";
import {AuthService} from "@shared/services";
import {AuthServiceImpl} from "@features/auth/services";
import {AuthState} from "@shared/states";
import {AuthStateImpl} from "@features/auth/states";

const authServiceProvider: Provider = {provide: AuthService, useClass: AuthServiceImpl};

const authStateProvider: Provider = {provide: AuthState, useClass: AuthStateImpl};

export const authProviders = [
  authServiceProvider,
  authStateProvider
];
