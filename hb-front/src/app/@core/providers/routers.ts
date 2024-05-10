import {Provider} from "@angular/core";
import {RouterService} from "@core/abstracts";
import {RouterServiceImpl} from "@core/services";

const routerProvider: Provider = {provide: RouterService, useClass: RouterServiceImpl}

export const routersProviders: Provider[] = [
    routerProvider
];

