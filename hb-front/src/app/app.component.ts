import {Component, HostBinding} from "@angular/core";
import {RouterModule, RouterOutlet} from "@angular/router";
import {CommonModule} from "@angular/common";
import {MatIconModule} from "@angular/material/icon";
import {MatListModule} from "@angular/material/list";
import {MatButtonModule} from "@angular/material/button";
import {Navigation, toActivities, toLogin, toReports, toStates, toUsers} from "@core/models";
import {NavigationBarComponent} from "@ui/widgets";
import {map} from "rxjs";
import {AuthState} from "@core/states";

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
  ]
})
export class AppComponent {
  @HostBinding('class.host-class') addHostClass = true;

  constructor(
    private readonly _authState: AuthState,
  ) {
  }

  protected readonly navItems: Navigation[] = [
    {text: "Отчёты", icon: "stack_star", fn: () => void 0, path: toReports},
    {text: "ППС", icon: "groups", fn: () => void 0, path: toUsers},
    {
      text: "Штат", icon: "add_task", fn: () => void 0,
      path: toStates
    },
    {
      text: "События", icon: "emoji_objects", fn: () => void 0,
      path: toActivities
    },
    {
      text: "Выход", icon: "logout", fn: () => this._authState.removeToken(),
      path: toLogin
    }
  ];
  protected readonly valid$ = this._authState.valid$;
  protected readonly invalid$ = this.valid$.pipe(map((v) => !v));
}
