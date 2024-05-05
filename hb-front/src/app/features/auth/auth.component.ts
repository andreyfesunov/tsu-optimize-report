import {Component} from "@angular/core";
import {RouterOutlet} from "@angular/router";

@Component({
  selector: "app-work-area",
  standalone: true,
  template: "<router-outlet></router-outlet>",
  imports: [RouterOutlet]
})
export class AuthComponent {

}
