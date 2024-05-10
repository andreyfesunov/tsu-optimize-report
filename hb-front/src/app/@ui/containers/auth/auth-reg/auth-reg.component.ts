import {Component} from "@angular/core";
import {RouterService} from "@core/abstracts/services/router.service";
import {AuthRegFormComponent, SubscriptionComponent} from "@ui/widgets";
import {toLogin} from "@core/models";
import {AuthService} from "@core/abstracts";

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
    public constructor(
        private readonly _authService: AuthService,
        private readonly _routerService: RouterService,
    ) {
        super();
    }

    protected onSubmit(params: { email: string; password: string }): void {
        this.subscription.add(this._authService.reg(params).subscribe(() => this.onLogin()));
    }

    protected onLogin(): void {
        this._routerService.navigate(toLogin);
    }
}
