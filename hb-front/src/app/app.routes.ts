import {Routes} from "@angular/router";
import {authGuard, notAuthGuard} from "@core/guards";
import {AppRoutes} from "@core/models";
import {MainRoutes} from "@core/models/routes/enums/main.routes";

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
