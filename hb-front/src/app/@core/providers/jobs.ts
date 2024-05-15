import {Provider} from "@angular/core";
import {JobsService} from "@core/abstracts";
import {JobsImplService} from "@core/services";

const jobsServiceProvider: Provider = {provide: JobsService, useClass: JobsImplService};

export const jobsProviders = [jobsServiceProvider];
