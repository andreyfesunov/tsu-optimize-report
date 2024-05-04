import {Component} from "@angular/core";
import {AuthLoginFormComponent} from "@features/auth/components/auth-login-form/auth-login-form.component";
import { AuthServiceImpl } from "@features/auth/services";

@Component({
  standalone: true,
  imports: [
    AuthLoginFormComponent,
  ],
  template: `
    <app-auth-login-form (submitEvent)="onSubmit($event)"></app-auth-login-form>
  `
})
export class AuthLoginComponent {

  public constructor(
    private readonly _authService: AuthServiceImpl,
  ) {
  }
  // хост класс всё ламаит :(
  // @HostBinding('class.host-class') addHostClass = true;

  // how to use distinctUntilChanged ? where is subscribe on event?
  protected onSubmit(eventData: {login: string, password: string}): void {
    console.log(eventData.login + " | " + eventData.password);
    this._authService.logIn(eventData);
  }
}
