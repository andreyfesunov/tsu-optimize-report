import {Routes} from "@angular/router";
import {UsersListComponent} from "@ui/containers/users/users-list/users-list.component";
import {authGuard, roleGuard} from "@core/guards";
import {RoleEnum} from "@core/models";

export const routes: Routes = [
  {
    path: '',
    component: UsersListComponent,
    canActivate: [roleGuard(RoleEnum.ADMIN), authGuard]
  }
]
