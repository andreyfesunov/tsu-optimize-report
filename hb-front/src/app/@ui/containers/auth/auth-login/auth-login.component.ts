import {Component} from "@angular/core";
import {SubscriptionComponent} from "@ui/widgets/utils/subscription/subscription.component";
import {RouterService} from "@core/abstracts/services/router.service";
import {IUser, toReg} from "@core/models";
import {AuthLoginFormComponent} from "@ui/widgets";
import {AuthService, AuthState} from "@core/abstracts";

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
        private readonly _routerService: RouterService
    ) {
        super();
    }

    protected onSubmit(eventData: { email: string, password: string }): void {
        this.subscription.add(this._authService.logIn(eventData).subscribe((res) => {
            console.log(res.token);
            this._authState.setToken(res.token);
            // this._routerNORMAL.navigate(["main"]);
        }));
    }

    protected onRegister(): void {
        this._routerService.navigate(toReg);
    }
}
