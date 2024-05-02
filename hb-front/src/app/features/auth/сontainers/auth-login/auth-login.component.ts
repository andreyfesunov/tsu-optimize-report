import {Component, HostBinding} from "@angular/core";
import {AuthLoginFormComponent} from "@features/auth/components/auth-login-form/auth-login-form.component";
import {AuthWrapperComponent} from "@features/auth/components/auth-wrapper/auth-wrapper.component";

@Component({
  standalone: true,
  imports: [
    AuthLoginFormComponent,
    AuthWrapperComponent
  ],
  template: `
    <app-auth-wrapper>
      <app-auth-login-form (submitEvent)="onSubmit()"></app-auth-login-form>
    </app-auth-wrapper>
  `
})
export class AuthLoginComponent {
  @HostBinding('class.host-class') addHostClass = true;

  protected onSubmit(): void {

  }
}
