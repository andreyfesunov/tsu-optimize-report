import {Routes} from "@angular/router";
import {authGuard, notAuthGuard} from "@core/guards";
import {AppRoutes, MainRoutes} from "@core/models";

export const routes: Routes = [
  {
    path: AppRoutes.AUTH,
    loadChildren: () => import("@ui/containers/auth/auth.routes").then(m => m.routes),
    canActivate: [notAuthGuard]
  },
  {
    path: AppRoutes.MAIN,
    canActivate: [authGuard],
    children: [
      {
        path: MainRoutes.REPORTS,
        loadChildren: () => import("@ui/containers/reports/reports.routes").then(m => m.routes)
      },
      {
        path: MainRoutes.USERS,
        loadChildren: () => import("@ui/containers/users/users.routes").then(m => m.routes)
      },
      {
        path: '',
        pathMatch: 'full',
        redirectTo: MainRoutes.REPORTS
      }
    ]
  },
  {
    path: "",
    pathMatch: "full",
    redirectTo: AppRoutes.AUTH
  }
];
