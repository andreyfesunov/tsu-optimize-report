import {Directive, OnDestroy} from "@angular/core";
import {Subscription} from "rxjs";

@Directive()
export class SubscriptionComponent implements OnDestroy {
    protected readonly subscription: Subscription = new Subscription();

    public ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }
}
