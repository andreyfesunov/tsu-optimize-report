import {TableRowController} from "@core/controllers";
import {Component} from "@angular/core";
import {IActivity} from "@core/models";
import {NgForOf, NgIf, NgSwitch, NgSwitchCase, NgSwitchDefault} from "@angular/common";

@Component({
  selector: 'tr[app-reports-first-half-table-row]',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    NgSwitchCase,
    NgSwitch,
    NgSwitchDefault
  ],
  template: `
    <td
      *ngFor="let col of cols"
      class="tsu-table-td"
      [class.tsu-table-td--hovered]="hovered"
      [class.tsu-table-td--center]="col.align === 'center'"
    >
      <ng-container *ngIf="item" [ngSwitch]="col.id">
        <ng-container *ngSwitchCase="FirstHalfItemField.LESSON">{{ item.lessonTypeName }}</ng-container>
        <ng-container *ngSwitchDefault>{{ getActivityHours(col.id) }}</ng-container>
      </ng-container>
    </td>
  `
})
export class ReportsFirstHalfTableRowComponent extends TableRowController<IRecordEntry, string> {
  protected readonly FirstHalfItemField = FirstHalfItemField;

  protected getActivityHours(id: string) {
    const item = this.item;

    if (item === null) return 0;

    return item.activities.find((v) => v.id === id)?.hours ?? 0;
  }
}

export enum FirstHalfItemField {
  LESSON = 'LESSON'
}

export interface IRecordEntry {
  readonly lessonTypeName: string;
  readonly activities: readonly (IActivity & { hours: number })[];
}
