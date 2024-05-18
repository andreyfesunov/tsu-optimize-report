import {Provider} from "@angular/core";
import {ReportsDialogService, ReportsService} from "@core/abstracts";
import {ReportsDialogImplService, ReportsImplService} from "@core/services";

const reportServiceProvider: Provider = {provide: ReportsService, useClass: ReportsImplService}

const reportDialogServiceProvider: Provider = {provide: ReportsDialogService, useClass: ReportsDialogImplService}

export const reportsProviders = [
  reportServiceProvider,
  reportDialogServiceProvider
]
