import { TableRowController } from "@core/controllers";
import { IUserState } from "@core/models";
import { Component } from "@angular/core";
import { NgForOf, NgIf, NgSwitch, NgSwitchCase } from "@angular/common";

export enum UsersTableRowItemField {
  FIRSTNAME = 'FIRSTNAME',
  LASTNAME = 'LASTNAME',
  EMAIL = 'EMAIL',
  EXPAND = 'EXPAND'
}

@Component({
  selector: 'tr[app-users-table-row]',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    NgSwitchCase,
    NgSwitch
  ],
  template: `
    <td
      *ngFor="let col of cols"
      class="tsu-table-td"
      [class.tsu-table-td--hovered]="hovered"
      [class.tsu-table-td--center]="col.align === 'center'"
    >
      <ng-container *ngIf="item" [ngSwitch]="col.id">
        <ng-container *ngSwitchCase="UsersTableRowItemField.EXPAND">
          <button class="expand-button" (click)="item.expanded = !item.expanded">
            {{ item.expanded ? '▼' : '▶' }}
          </button>
        </ng-container>
        <ng-container *ngSwitchCase="UsersTableRowItemField.FIRSTNAME">{{ item.user.firstname }}</ng-container>
        <ng-container *ngSwitchCase="UsersTableRowItemField.LASTNAME">{{ item.user.lastname }}</ng-container>
        <ng-container *ngSwitchCase="UsersTableRowItemField.EMAIL">{{ item.user.email }}</ng-container>
      </ng-container>
    </td>
  `,
  styles: [`
    .expand-button {
      background: none;
      border: none;
      cursor: pointer;
      padding: 4px 8px;
    }
  `]
})
export class UsersTableRowComponent extends TableRowController<IUserState, UsersTableRowItemField> {
  protected readonly UsersTableRowItemField = UsersTableRowItemField;
}
