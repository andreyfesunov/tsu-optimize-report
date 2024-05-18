import {Provider} from "@angular/core";
import {InstitutesService} from "@core/abstracts";
import {InstitutesImplService} from "@core/services";

const institutesServiceProvider: Provider = {provide: InstitutesService, useClass: InstitutesImplService}

export const institutesProviders = [
  institutesServiceProvider
];
