import {Component} from "@angular/core";
import {toMain, toReg} from "@core/models";
import {AuthLoginFormComponent} from "@ui/widgets";
import {ILoginRegDto} from "@core/dtos";
import {SubscriptionController} from "@core/controllers";
import {AuthService, RouterService} from "@core/services";
import {AuthState} from "@core/states";

@Component({
  standalone: true,
  imports: [
    AuthLoginFormComponent,
  ],
  template: `
    <app-auth-login-form (redirectEvent)="onRegister()" (submitEvent)="onSubmit($event)"></app-auth-login-form>
  `,
  host: {class: 'host-class'}
})
export class AuthLoginComponent extends SubscriptionController {
  public constructor(
    private readonly _authService: AuthService,
    private readonly _authState: AuthState,
    private readonly _routerService: RouterService
  ) {
    super();
  }

  protected onSubmit(eventData: ILoginRegDto): void {
    this.subscription.add(this._authService.logIn(eventData).subscribe((res) => {
      this._authState.setToken(res.token);
      this._routerService.navigate(toMain);
    }));
  }

  protected onRegister(): void {
    this._routerService.navigate(toReg);
  }
}
