import {Provider} from "@angular/core";
import {StatesDialogService, StatesService} from "@core/abstracts";
import {StatesDialogImplService, StatesImplService} from "@core/services";

const statesServiceProvider: Provider = {provide: StatesService, useClass: StatesImplService};

const statesServiceDialogProvider: Provider = {provide: StatesDialogService, useClass: StatesDialogImplService};

export const statesProviders: Provider[] = [
  statesServiceProvider,
  statesServiceDialogProvider
];
