import {Provider} from "@angular/core";
import {ReportsService} from "@core/abstracts";
import {ReportsImplService} from "@core/services";

const reportServiceProvider: Provider = {provide: ReportsService, useClass: ReportsImplService}

export const reportsProviders = [
  reportServiceProvider
]
