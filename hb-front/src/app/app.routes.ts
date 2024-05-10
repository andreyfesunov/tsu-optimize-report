import {Routes} from "@angular/router";
import {ReportsTabsComponent} from "@ui/widgets/reports/reports-tabs/reports-tabs.component";
import {authGuard, notAuthGuard} from "@core/guards";
import {AppRoutes} from "@core/models";

export const routes: Routes = [
    {
        path: AppRoutes.AUTH,
        loadChildren: () => import("@ui/containers/auth/auth.routes").then(m => m.routes),
        canActivate: [notAuthGuard]
    },
    {
        path: AppRoutes.MAIN,
        component: ReportsTabsComponent,
        canActivate: [authGuard],
    },
    {
        path: "",
        pathMatch: "full",
        redirectTo: AppRoutes.AUTH
    }
];
