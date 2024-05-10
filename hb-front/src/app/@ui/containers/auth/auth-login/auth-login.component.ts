import {Component} from "@angular/core";
import {SubscriptionComponent} from "@ui/widgets/utils/subscription/subscription.component";
import {RouterService} from "@core/abstracts/services/router.service";
import {ITokenData, toMain, toReg} from "@core/models";
import {AuthLoginFormComponent} from "@ui/widgets";
import {AuthService, AuthState} from "@core/abstracts";
import {ILoginRegDto} from "@core/dtos";

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
        private readonly _authState: AuthState<ITokenData>,
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
