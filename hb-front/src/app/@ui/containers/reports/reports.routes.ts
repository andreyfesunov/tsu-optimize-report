import {Routes} from "@angular/router";
import {ReportsListComponent} from "@ui/containers/reports/reports-list/reports-list.component";
import {ReportDetailComponent} from "@ui/containers/reports/report-detail/report-detail.component";
import {ParamsRoutes, RoleEnum} from "@core/models";
import {authGuard, roleGuard} from "@core/guards";

export const routes: Routes = [
  {
    path: '',
    component: ReportsListComponent,
    canActivate: [roleGuard(RoleEnum.USER), authGuard]
  },
  {
    path: `:${ParamsRoutes.ID}`,
    component: ReportDetailComponent,
    canActivate: [roleGuard(RoleEnum.USER), authGuard]
  }
]
