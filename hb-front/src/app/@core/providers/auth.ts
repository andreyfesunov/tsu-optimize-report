import {Provider} from "@angular/core";
import {AuthService, AuthState} from "@core/abstracts";
import {AuthServiceImpl} from "@core/services";
import {AuthImplState} from "@core/states";
import {ITokenData} from "@core/models";

const authServiceProvider: Provider = {provide: AuthService, useClass: AuthServiceImpl};

const authStateProvider: Provider = {provide: AuthState<ITokenData>, useClass: AuthImplState};

export const authProviders = [
    authStateProvider,
    authServiceProvider
]
