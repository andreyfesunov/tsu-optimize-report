import {toMain} from "@core/models/routes/constants/app.routes";
import {MainRoutes} from "@core/models";

export const toReports = [...toMain, MainRoutes.REPORTS];

export const toUsers = [...toMain, MainRoutes.USERS];

export const toStates = [...toMain, MainRoutes.STATES];
