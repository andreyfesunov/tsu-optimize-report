import {Provider} from "@angular/core";
import {UsersService} from "@core/abstracts";
import {UsersImplService} from "@core/services";

const usersServiceProvider: Provider = {provide: UsersService, useClass: UsersImplService};

export const usersProviders: Provider[] = [
  usersServiceProvider
]
