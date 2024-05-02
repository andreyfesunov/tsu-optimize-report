import {Routes} from "@angular/router";
import {AuthLoginComponent} from "./components/auth-login/auth-login.component";
import {AuthRegComponent} from "./components/auth-reg/auth-reg.component";

export enum AuthRoutes {
  LOGIN = 'login',
  REG = 'reg'
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
  {
    path: '',
    pathMatch: 'full',
    redirectTo: AuthRoutes.LOGIN
  }
]
