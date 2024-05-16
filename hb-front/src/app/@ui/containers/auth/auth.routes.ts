import {Routes} from "@angular/router";
import {AuthRoutes} from "@core/models";
import {AuthLoginComponent} from "@ui/containers/auth/auth-login/auth-login.component";
import {AuthRegComponent} from "@ui/containers/auth/auth-reg/auth-reg.component";

export const routes: Routes = [
  {
    path: "",
    pathMatch: "full",
    redirectTo: AuthRoutes.LOGIN
  },
  {
    path: AuthRoutes.LOGIN,
    component: AuthLoginComponent
  },
  {
    path: AuthRoutes.REG,
    component: AuthRegComponent
  }
]
