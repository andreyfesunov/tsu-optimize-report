import {Routes} from "@angular/router";
import {StatesListComponent} from "@ui/containers/states/states-list/states-list.component";
import {authGuard, roleGuard} from "@core/guards";
import {RoleEnum} from "@core/models";

export const routes: Routes = [
  {
    path: '',
    component: StatesListComponent,
    canActivate: [roleGuard(RoleEnum.ADMIN), authGuard]
  }
]
