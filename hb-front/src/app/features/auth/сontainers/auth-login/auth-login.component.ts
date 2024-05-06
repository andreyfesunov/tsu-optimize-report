import {Component, Inject} from "@angular/core";
import {AuthLoginFormComponent} from "@features/auth/components/auth-login-form/auth-login-form.component";
import {SubscriptionComponent} from "@shared/components/subscription.component";
import {AuthService} from "@shared/services";
import {AUTH_ROUTER} from "@shared/tokens/routers";
import {RouterService} from "@shared/services/router.service";
import {AuthRoutes} from "@features/auth/auth.routes";
import { Router } from "@angular/router";
import { AuthState, IUser } from "@shared/index";

@Component({
  standalone: true,
  imports: [
    AuthLoginFormComponent,
  ],
  template: `
    <app-auth-login-form (redirectEvent)="onRegister()" (submitEvent)="onSubmit($event)"></app-auth-login-form>
  `
})
export class AuthLoginComponent extends SubscriptionComponent {
  public constructor(
    private readonly _authService: AuthService,
    private readonly _authState: AuthState<IUser>,
    @Inject(AUTH_ROUTER) private readonly _routerService: RouterService,
    private readonly _routerNORMAL: Router
  ) {
    super();
  }

  protected onSubmit(eventData: { email: string, password: string }): void {
    this.subscription.add(this._authService.logIn(eventData).subscribe((res) => {
      console.log(res.token);
      this._authState.setToken(res.token);
      this._routerNORMAL.navigate(["main"]);
    }));
  }

  protected onRegister(): void {
    this._routerNORMAL.navigate(["reg"]);
    // this._routerService.navigate([AuthRoutes.REG]);
  }
}
