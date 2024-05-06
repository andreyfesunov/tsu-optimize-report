import { Component } from "@angular/core";
import { Router, RouterModule, RouterOutlet } from "@angular/router";
import { AuthState, IUser } from "./@shared";
import { CommonModule } from "@angular/common";
import { NavigationComponent } from "./@shared/components/navigation/navigation.component";
import { MatIconModule } from "@angular/material/icon";
import { MatListModule } from "@angular/material/list";
import { MatButtonModule } from "@angular/material/button";

@Component({
    selector: "app-root",
    standalone: true,
    templateUrl: "./app.component.html",
    styleUrl: "./app.component.scss",
    imports: [
        CommonModule,
        RouterOutlet,
        RouterModule,
        NavigationComponent,
        MatListModule,
        MatIconModule,
        MatButtonModule
    ]
})
export class AppComponent  {

  protected readonly isAutorized!: boolean;

  constructor(
    private _authState: AuthState<IUser>,
    private _router: Router
  ) { }

  public isAuthorizedFn(): boolean {
    let test = this._authState.isTokenValid();
    return test;
  }
}
