import {Provider} from "@angular/core";
import {StatesService} from "@core/abstracts";
import {StatesImplService} from "@core/services";

const statesServiceProvider: Provider = {provide: StatesService, useClass: StatesImplService};

export const statesProviders: Provider[] = [
  statesServiceProvider
];
