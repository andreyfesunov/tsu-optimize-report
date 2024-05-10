import {toAuth} from "@core/models/routes/constants/app.routes";
import {AuthRoutes} from "../enums";

export const toLogin = [...toAuth, AuthRoutes.LOGIN];

export const toReg = [...toAuth, AuthRoutes.REG];
