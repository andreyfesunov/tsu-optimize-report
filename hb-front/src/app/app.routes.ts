import { Routes } from "@angular/router";
import { authGuard } from "@features/auth/guards";
import { AuthLoginComponent } from "@features/auth/сontainers/auth-login/auth-login.component";
import { AuthRegComponent } from "@features/auth/сontainers/auth-reg/auth-reg.component";
import { ReportsComponent } from "@shared/components/reports/reports.component";

export enum AppRoutes {
  AUTH = "auth",
  ADMIN = "adm",
  USER = "usr"
}

export const routes: Routes = [
  // {
  //   path: AppRoutes.AUTH,
  //   loadChildren: () => import("@features/auth/auth.routes").then(m => m.routes),
  //   providers: []
  // },
  // {
  //   path: "",
  //   pathMatch: "full",
  //   redirectTo: AppRoutes.AUTH
  // },
  // {
  //   path: 'main',
  //   component: ReportsComponent,
  //   canActivate: [authGuard],
  // },
  {
    path: "login",
    component: AuthLoginComponent
  },
  {
    path: "reg",
    component: AuthRegComponent
  },
  {
    path: "",
    pathMatch: "full",
    redirectTo: "main",
  },
  {
    path: 'main',
    component: ReportsComponent,
    canActivate: [authGuard]
  },
];
