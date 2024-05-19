import {TableRowController} from "@core/controllers";
import {IEventType} from "@core/models";
import {Component} from "@angular/core";
import {NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";

export enum ActivitiesTableRowItemField {
  NAME = 'NAME',
  DESCRIPTION = 'DESCRIPTION'
}

@Component({
  selector: 'tr[app-activities-table-row]',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    NgSwitch,
    NgSwitchCase
  ],
  template: `
    <td
      *ngFor="let col of cols"
      class="tsu-table-td"
      [class.tsu-table-td--hovered]="hovered"
      [class.tsu-table-td--center]="col.align === 'center'"
    >
      <ng-container *ngIf="item" [ngSwitch]="col.id">
        <ng-container *ngSwitchCase="ActivitiesTableRowItemField.NAME">{{ item.name }}</ng-container>
        <ng-container *ngSwitchCase="ActivitiesTableRowItemField.DESCRIPTION">{{ item.description }}</ng-container>
      </ng-container>
    </td>
  `
})
export class ActivitiesTableRowComponent extends TableRowController<IEventType, ActivitiesTableRowItemField> {
  protected readonly ActivitiesTableRowItemField = ActivitiesTableRowItemField;
}
