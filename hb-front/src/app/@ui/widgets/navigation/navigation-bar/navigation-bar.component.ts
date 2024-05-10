import {CommonModule} from '@angular/common';
import {Component} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import {Router, RouterModule} from '@angular/router';
import {IUser} from '@core/models';
import {AuthState} from "@core/abstracts";

@Component({
    selector: 'app-navigation-bar',
    standalone: true,
    imports: [
        CommonModule,
        MatListModule,
        MatButtonModule,
        MatIconModule,
        RouterModule,
    ],
    template: `
        <div class="navigation">
            <nav class="navigation__category">
                <a mat-button class="item-wrapper" routerLink="main" routerLink="main"
                   routerLinkActive="mdc-list-item--activated">
                    <div class="navigation__category__item">
                        <mat-icon class="navigation__category__item__icon material-symbols-outlined">person</mat-icon>
                        <span>Профиль</span>
                    </div>
                </a>
                <a mat-button class="item-wrapper" routerLink="products" routerLinkActive="mdc-list-item--activated">
                    <div class="navigation__category__item">
                        <mat-icon class="navigation__category__item__icon material-symbols-outlined">grocery</mat-icon>
                        <span>Продукты</span>
                    </div>
                </a>
                <a mat-button class="item-wrapper" routerLink="calendar" routerLinkActive="mdc-list-item--activated">
                    <div class="navigation__category__item">
                        <mat-icon class="navigation__category__item__icon material-symbols-outlined">calendar_month
                        </mat-icon>
                        <span>Календарь</span>
                    </div>
                </a>
                <a mat-button class="item-wrapper" (click)="logout();">
                    <div class="navigation__category__item">
                        <mat-icon class="navigation__category__item__icon material-symbols-outlined">logout</mat-icon>
                        <span>Выйти</span>
                    </div>
                </a>
            </nav>
        </div>
    `,
    styleUrl: './navigation-bar.component.scss'
})
export class NavigationBarComponent {
    constructor(
        private readonly _authState: AuthState<IUser>,
        private readonly _router: Router
    ) {
    }

    public logout(): void {
        this._authState.removeToken();
        this._router.navigate(["login"]);
    }
}
