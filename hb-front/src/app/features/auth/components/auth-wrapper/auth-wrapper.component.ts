import {Component, HostBinding} from "@angular/core";

@Component({
  selector: 'app-auth-wrapper',
  standalone: true,
  template: `
    <div class="app-auth-wrapper">
      <ng-content></ng-content>
    </div>
  `,
  styles: [
    `
      .app-auth-wrapper {
        background-color: red;
      }
    `
  ]
})
export class AuthWrapperComponent {
  @HostBinding('class.host-class') addHostClass = true;
}
