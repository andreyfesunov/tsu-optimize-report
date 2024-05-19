import {Provider} from "@angular/core";
import {EventTypesService} from "@core/abstracts";
import {EventTypesImplService} from "@core/services";

const eventTypesServiceProvider: Provider = {provide: EventTypesService, useClass: EventTypesImplService};

export const eventTypesProviders = [
  eventTypesServiceProvider
];
