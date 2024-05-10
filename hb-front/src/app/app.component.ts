import {Component} from "@angular/core";
import {RouterModule, RouterOutlet} from "@angular/router";
import {CommonModule} from "@angular/common";
import {MatIconModule} from "@angular/material/icon";
import {MatListModule} from "@angular/material/list";
import {MatButtonModule} from "@angular/material/button";
import {AuthState} from "@core/abstracts";
import {IUser} from "@core/models";
import {NavigationBarComponent} from "@ui/widgets";

@Component({
    selector: "app-root",
    standalone: true,
    templateUrl: "./app.component.html",
    styleUrl: "./app.component.scss",
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
    constructor(
        private _authState: AuthState<IUser>,
    ) {
    }

    public isAuthorizedFn(): boolean {
        return this._authState.isTokenValid();
    }
}
