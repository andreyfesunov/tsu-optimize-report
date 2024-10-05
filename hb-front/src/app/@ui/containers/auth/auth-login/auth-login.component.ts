import {Component, inject} from "@angular/core";
import {toMain, toReg} from "@core/models";
import {AuthLoginFormComponent} from "@ui/widgets";
import {ILoginDto} from "@core/dtos";
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
  private readonly _authService = inject(AuthService);
  private readonly _authState = inject(AuthState);
  private readonly _routerService = inject(RouterService);

  protected onSubmit(eventData: ILoginDto): void {
    this.subscription.add(this._authService.logIn(eventData).subscribe((res) => {
      this._authState.setToken(res.token);
      this._routerService.navigate(toMain);
    }));
  }

  protected onRegister(): void {
    this._routerService.navigate(toReg);
  }
}
