import {CommonModule} from '@angular/common';
import {Component, Input} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import {RouterModule} from '@angular/router';
import {Navigation} from "@core/models";

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
        <a *ngFor="let item of items" mat-button class="item-wrapper" [routerLink]="item.path"
           routerLinkActive="active" (click)="item.fn()">
          <div class="navigation__category__item">
            <mat-icon
              class="navigation__category__item__icon material-symbols-outlined">{{ item.icon }}
            </mat-icon>
            <span>{{ item.text }}</span>
          </div>
        </a>
      </nav>
    </div>
  `,
  styleUrl: './navigation-bar.component.scss'
})
export class NavigationBarComponent {
  @Input() public items: Navigation[] = [];
}
