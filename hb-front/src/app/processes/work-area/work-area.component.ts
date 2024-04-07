import {Component} from "@angular/core";
import {RouterOutlet} from "@angular/router";
import {AuthState} from "../../shared";
import {AuthStateImpl} from "./states/auth-state.impl";

@Component({
  selector: 'app-work-area',
  standalone: true,
  template: '<router-outlet></router-outlet>',
  imports: [RouterOutlet],
  providers: [
    {provide: AuthState, useClass: AuthStateImpl}
  ]
})
export class WorkAreaComponent {

}
