import {Routes} from "@angular/router";

export enum AppRoutes {
  AUTH = "auth",
  ADMIN = "adm",
  USER = "usr"
}

export const routes: Routes = [
  {
    path: AppRoutes.AUTH,
    loadChildren: () => import("@features/auth/auth.routes").then(m => m.routes),
    providers: []
  },
  {
    path: "",
    pathMatch: "full",
    redirectTo: AppRoutes.AUTH
  }
];
