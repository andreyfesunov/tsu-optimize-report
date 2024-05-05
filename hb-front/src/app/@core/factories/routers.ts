import {Router} from "@angular/router";
import {RouterServiceImpl} from "@core/services";
import {AppRoutes} from "../../app.routes";

export const authRouterFactory = (router: Router) => new RouterServiceImpl(router, [AppRoutes.AUTH]);
