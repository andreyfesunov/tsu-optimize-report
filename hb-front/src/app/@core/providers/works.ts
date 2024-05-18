import {Provider} from "@angular/core";
import {WorksService} from "@core/abstracts";
import {WorksImplService} from "@core/services";

const worksServiceProvider: Provider = {provide: WorksService, useClass: WorksImplService};

export const worksProviders = [
  worksServiceProvider
];
