import {toMain} from "@core/models";
import {MainRoutes} from "@core/models/routes/enums/main.routes";

export const toReports = [...toMain, MainRoutes.REPORTS];
