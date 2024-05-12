import {TableRowController} from "@core/controllers";
import {IUser} from "@core/models";
import {Component} from "@angular/core";
import {NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";

export enum UsersTableRowItemField {
  FIRSTNAME = 'FIRSTNAME',
  LASTNAME = 'LASTNAME',
  EMAIL = 'EMAIL'
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
        <ng-container *ngSwitchCase="UsersTableRowItemField.FIRSTNAME">{{ item.firstname }}</ng-container>
        <ng-container *ngSwitchCase="UsersTableRowItemField.LASTNAME">{{ item.lastname }}</ng-container>
        <ng-container *ngSwitchCase="UsersTableRowItemField.EMAIL">{{ item.email }}</ng-container>
      </ng-container>
    </td>
  `
})
export class UsersTableRowComponent extends TableRowController<IUser, UsersTableRowItemField> {
  protected readonly UsersTableRowItemField = UsersTableRowItemField;
}
