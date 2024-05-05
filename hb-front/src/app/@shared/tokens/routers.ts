import {InjectionToken} from "@angular/core";
import {RouterService} from "@shared/services/router.service";

export const AUTH_ROUTER = new InjectionToken<RouterService>("AUTH_ROUTER_SERVICE");
