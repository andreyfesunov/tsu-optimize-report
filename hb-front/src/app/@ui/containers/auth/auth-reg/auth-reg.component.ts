import {Component, inject} from "@angular/core";
import {AuthRegFormComponent} from "@ui/widgets";
import {toLogin} from "@core/models";
import {ILoginDto} from "@core/dtos";
import {SubscriptionController} from "@core/controllers";
import {AuthService, RouterService} from "@core/services";

@Component({
  standalone: true,
  imports: [
    AuthRegFormComponent,
  ],
  template: `
    <app-auth-reg-form (redirectEvent)="onLogin()" (submitEvent)="onSubmit($event)"></app-auth-reg-form>
  `,
  host: {class: 'host-class'}

})
export class AuthRegComponent extends SubscriptionController {
  private readonly _authService = inject(AuthService);
  private readonly _routerService = inject(RouterService);

  protected onSubmit(params: ILoginDto): void {
    this.subscription.add(this._authService.reg(params).subscribe(() => this.onLogin()));
  }

  protected onLogin(): void {
    this._routerService.navigate(toLogin);
  }
}
