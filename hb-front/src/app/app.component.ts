import {Component, HostBinding} from "@angular/core";
import {RouterModule, RouterOutlet} from "@angular/router";
import {CommonModule} from "@angular/common";
import {MatIconModule} from "@angular/material/icon";
import {MatListModule} from "@angular/material/list";
import {MatButtonModule} from "@angular/material/button";
import {AuthState} from "@core/abstracts";
import {ITokenData} from "@core/models";
import {NavigationBarComponent} from "@ui/widgets";
import {map} from "rxjs";

@Component({
    selector: "app-root",
    standalone: true,
    templateUrl: "app.component.html",
    styleUrl: "app.component.scss",
    imports: [
        CommonModule,
        RouterOutlet,
        RouterModule,
        NavigationBarComponent,
        MatListModule,
        MatIconModule,
        MatButtonModule
    ]
})
export class AppComponent {
    @HostBinding('class.host-class') addHostClass = true;

    constructor(
        private readonly _authState: AuthState<ITokenData>,
    ) {
    }

    protected readonly valid$ = this._authState.valid$;
    protected readonly invalid$ = this.valid$.pipe(map((v) => !v));
}
