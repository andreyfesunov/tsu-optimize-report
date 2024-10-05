import {Component, inject} from "@angular/core";
import {RouterModule, RouterOutlet} from "@angular/router";
import {CommonModule} from "@angular/common";
import {MatIconModule} from "@angular/material/icon";
import {MatListModule} from "@angular/material/list";
import {MatButtonModule} from "@angular/material/button";
import {Navigation, RoleEnum, toActivities, toLogin, toReports, toStates, toUsers} from "@core/models";
import {NavigationBarComponent} from "@ui/widgets";
import {map} from "rxjs";
import {AuthState} from "@core/states";
import {roleFn$} from "@core/guards";

@Component({
  selector: "app-root",
  standalone: true,
  templateUrl: "app.component.html",
  styleUrl: "app.component.scss",
  imports: [
    CommonModule,
    RouterOutlet,
    RouterModule,
    NavigationBarComponent,
    MatListModule,
    MatIconModule,
    MatButtonModule
  ],
  host: { class: 'host-class' }
})
export class AppComponent {
  private readonly _authState = inject(AuthState);

  protected readonly navItems: Navigation[] = [
    {
      text: "Отчёты",
      icon: "stack_star",
      fn: () => void 0,
      path: toReports,
      canActivate$: roleFn$(RoleEnum.USER)
    },
    {
      text: "ППС",
      icon: "groups",
      fn: () => void 0,
      path: toUsers,
      canActivate$: roleFn$(RoleEnum.ADMIN)
    },
    {
      text: "Штат",
      icon: "add_task",
      fn: () => void 0,
      path: toStates,
      canActivate$: roleFn$(RoleEnum.ADMIN)
    },
    {
      text: "События",
      icon: "emoji_objects",
      fn: () => void 0,
      path: toActivities,
      canActivate$: roleFn$(RoleEnum.ADMIN)
    },
    {
      text: "Выход",
      icon: "logout",
      fn: () => this._authState.removeToken(),
      path: toLogin,
      canActivate$: roleFn$(RoleEnum.USER)
    }
  ];
  protected readonly valid$ = this._authState.valid$;
  protected readonly invalid$ = this.valid$.pipe(map((v) => !v));
}
