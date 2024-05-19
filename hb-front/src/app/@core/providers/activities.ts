import {Provider} from "@angular/core";
import {ActivitiesDialogService, ActivitiesService} from "@core/abstracts";
import {ActivitiesDialogImplService, ActivitiesImplService} from "@core/services";

const activitiesServiceProvider: Provider = {provide: ActivitiesService, useClass: ActivitiesImplService};

const activitiesDialogServiceProvider: Provider = {
  provide: ActivitiesDialogService,
  useClass: ActivitiesDialogImplService
}

export const activitiesProviders = [
  activitiesServiceProvider,
  activitiesDialogServiceProvider
];
