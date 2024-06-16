import {Routes} from "@angular/router";
import {ActivitiesListComponent} from "@ui/containers/activities/activities-list/activities-list.component";
import {authGuard, roleGuard} from "@core/guards";
import {RoleEnum} from "@core/models";

export const routes: Routes = [
  {
    path: '',
    component: ActivitiesListComponent,
    canActivate: [roleGuard(RoleEnum.ADMIN), authGuard]
  }
];
