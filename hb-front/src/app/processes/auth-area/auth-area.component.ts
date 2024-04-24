import {Component} from "@angular/core";
import {RouterOutlet} from "@angular/router";
import {AuthService} from "../../shared";
import {AuthServiceImpl} from "./api";

@Component({
  selector: 'app-work-area',
  standalone: true,
  template: '<router-outlet></router-outlet>',
  imports: [RouterOutlet],
  providers: [
    {provide: AuthService, useClass: AuthServiceImpl}
  ]
})
export class AuthAreaComponent {

}
