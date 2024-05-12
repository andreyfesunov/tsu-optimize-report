import {Provider} from "@angular/core";
import {ReportService} from "@core/abstracts";
import {ReportImplService} from "@core/services";

const reportServiceProvider: Provider = {provide: ReportService, useClass: ReportImplService}

export const reportsProviders = [
    reportServiceProvider
]
