import {Component} from "@angular/core";
import {RouterService} from "@core/abstracts/services/router.service";
import {AuthRegFormComponent} from "@ui/widgets";
import {toLogin} from "@core/models";
import {AuthService} from "@core/abstracts";
import {ILoginRegDto} from "@core/dtos";
import {SubscriptionController} from "@core/controllers";

@Component({
    standalone: true,
    imports: [
        AuthRegFormComponent,
    ],
    template: `
        <app-auth-reg-form (redirectEvent)="onLogin()" (submitEvent)="onSubmit($event)"></app-auth-reg-form>
    `
})
export class AuthRegComponent extends SubscriptionController {
    public constructor(
        private readonly _authService: AuthService,
        private readonly _routerService: RouterService,
    ) {
        super();
    }

    protected onSubmit(params: ILoginRegDto): void {
        this.subscription.add(this._authService.reg(params).subscribe(() => this.onLogin()));
    }

    protected onLogin(): void {
        this._routerService.navigate(toLogin);
    }
}
