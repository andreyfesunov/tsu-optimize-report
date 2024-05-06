import {Component, Inject} from "@angular/core";
import {AuthRegFormComponent} from "@features/auth/components/auth-reg-form/auth-reg-form.component";
import {AuthService} from "@shared/services";
import {SubscriptionComponent} from "@shared/components/subscription.component";
import {AUTH_ROUTER} from "@shared/tokens/routers";
import {RouterService} from "@shared/services/router.service";
import {AuthRoutes} from "@features/auth/auth.routes";
import { Router } from "@angular/router";

@Component({
  standalone: true,
  imports: [
    AuthRegFormComponent,
  ],
  template: `
    <app-auth-reg-form (redirectEvent)="onLogin()" (submitEvent)="onSubmit($event)"></app-auth-reg-form>
  `
})
export class AuthRegComponent extends SubscriptionComponent {
  constructor(
    private readonly _authService: AuthService,
    @Inject(AUTH_ROUTER) private readonly _routerService: RouterService,
    private readonly _routerNORMAL: Router
  ) {
    super();
  }

  protected onSubmit(params: { email: string; password: string }): void {
    this.subscription.add(this._authService.reg(params).subscribe());
    this.onLogin();
  }

  protected onLogin(): void {  
    // this._routerService.navigate([AuthRoutes.LOGIN]);
    this._routerNORMAL.navigate(["login"]);
  }
}
