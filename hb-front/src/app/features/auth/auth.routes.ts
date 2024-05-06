import {Routes} from "@angular/router";
import {AuthLoginComponent} from "@features/auth/сontainers/auth-login/auth-login.component";
import {AuthRegComponent} from "@features/auth/сontainers/auth-reg/auth-reg.component";

export enum AuthRoutes {
  LOGIN = "login",
  REG = "reg"
}

export const routes: Routes = [
  {
    path: AuthRoutes.LOGIN,
    component: AuthLoginComponent
  },
  {
    path: AuthRoutes.REG,
    component: AuthRegComponent
  },
  // {
  //  //ВСЁ ЛОМАЕТ МНЕ Ё ПЕ РЕ СЕ ТЕ! :*-(
  //   path: "",
  //   pathMatch: "full",
  //   redirectTo: AuthRoutes.LOGIN
  // }
]
