import {AuthRoutes} from "../enums";
import {toAuth} from "@core/models";

export const toLogin = [...toAuth, AuthRoutes.LOGIN];

export const toReg = [...toAuth, AuthRoutes.REG];
