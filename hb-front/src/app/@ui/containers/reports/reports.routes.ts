import {Routes} from "@angular/router";
import {ReportsListComponent} from "@ui/containers/reports/reports-list/reports-list.component";
import {ReportDetailComponent} from "@ui/containers/reports/report-detail/report-detail.component";
import {ParamsRoutes} from "@core/models";

export const routes: Routes = [
  {
    path: '',
    component: ReportsListComponent
  },
  {
    path: `:${ParamsRoutes.ID}`,
    component: ReportDetailComponent
  }
]
