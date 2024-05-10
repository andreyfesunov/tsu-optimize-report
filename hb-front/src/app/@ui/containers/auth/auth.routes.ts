import {Routes} from "@angular/router";
import {AuthLoginComponent} from "./auth-login/auth-login.component";
import {AuthRegComponent} from "./auth-reg/auth-reg.component";
import {AuthRoutes} from "@core/models";

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
