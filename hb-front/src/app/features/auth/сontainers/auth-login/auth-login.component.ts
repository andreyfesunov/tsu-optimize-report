import {Component, Inject} from "@angular/core";
import {AuthLoginFormComponent} from "@features/auth/components/auth-login-form/auth-login-form.component";
import {SubscriptionComponent} from "@shared/components/subscription.component";
import {AuthService} from "@shared/services";
import {AUTH_ROUTER} from "@shared/tokens/routers";
import {RouterService} from "@shared/services/router.service";
import {AuthRoutes} from "@features/auth/auth.routes";

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
    @Inject(AUTH_ROUTER) private readonly _routerService: RouterService,
  ) {
    super();
  }

  protected onSubmit(eventData: { login: string, password: string }): void {
    this.subscription.add(this._authService.logIn(eventData).subscribe());
  }

  protected onRegister(): void {
    this._routerService.navigate([AuthRoutes.REG]);
  }
}
