import {TableRowController} from "@core/controllers";
import {IState} from "@core/models";
import {Component} from "@angular/core";
import {CommonModule, NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";

export enum StateTableRowItemField {
  JOB = 'JOB',
  COUNT = 'COUNT',
  HOURS = 'HOURS',
  START_DATE = 'START_DATE',
  END_DATE = 'END_DATE'
}

@Component({
  selector: 'tr[app-states-table-row]',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    NgSwitch,
    NgSwitchCase,
    CommonModule
  ],
  template: `
    <td
      *ngFor="let col of cols"
      class="tsu-table-td"
      [class.tsu-table-td--hovered]="hovered"
      [class.tsu-table-td--center]="col.align === 'center'"
    >
      <ng-container *ngIf="item" [ngSwitch]="col.id">
        <ng-container *ngSwitchCase="StateTableRowItemField.JOB">{{ item.job.name }}</ng-container>
        <ng-container *ngSwitchCase="StateTableRowItemField.COUNT">{{ item.count }}</ng-container>
        <ng-container *ngSwitchCase="StateTableRowItemField.HOURS">{{ item.hours }}</ng-container>
        <ng-container *ngSwitchCase="StateTableRowItemField.END_DATE">{{ item.endDate | date: "dd MM yyyy" }}</ng-container>
        <ng-container *ngSwitchCase="StateTableRowItemField.START_DATE">{{ item.startDate | date: "dd MM yyyy" }}</ng-container>
      </ng-container>
    </td>
  `
})
export class StatesTableRowComponent extends TableRowController<IState, StateTableRowItemField> {
  protected readonly StateTableRowItemField = StateTableRowItemField;
}
