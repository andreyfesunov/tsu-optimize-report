import {Provider} from "@angular/core";
import {AuthService} from "@shared/services";
import {AuthServiceImpl} from "@features/auth/services";
import {AuthState} from "@shared/states";
import {AuthStateImpl} from "@features/auth/states";

export function provideAuthService(): Provider {
  return {provide: AuthService, useClass: AuthServiceImpl}
}

export function provideAuthState(): Provider {
  return {provide: AuthState, useClass: AuthStateImpl};
}
