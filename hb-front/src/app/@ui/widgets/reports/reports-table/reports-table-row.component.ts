import {TableRowController} from "@core/controllers";
import {IReport} from "@core/models";
import {Component} from "@angular/core";
import {NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";

export enum ReportsTableRowItemField {
  JOB = 'JOB',
  DEPARTMENT = 'DEPARTMENT',
  INSTITUTE = 'INSTITUTE',
  RATE = 'RATE',
  HOURS = 'HOURS',
  START_DATE = 'START_DATE',
  END_DATE = 'END_DATE'
}

@Component({
  selector: 'tr[app-reports-table-row]',
  standalone: true,
  imports: [
    NgForOf,
    NgSwitch,
    NgSwitchCase,
    NgIf
  ],
  template: `
    <td
      *ngFor="let col of cols"
      class="tsu-table-td"
      [class.tsu-table-td--hovered]="hovered"
      [class.tsu-table-td--center]="col.align === 'center'"
    >
      <ng-container *ngIf="item" [ngSwitch]="col.id">
        <ng-container *ngSwitchCase="ReportsTableRowItemField.JOB">{{ item.state.job.name }}</ng-container>
        <ng-container *ngSwitchCase="ReportsTableRowItemField.RATE">{{ item.rate }}</ng-container>
        <ng-container
          *ngSwitchCase="ReportsTableRowItemField.INSTITUTE">{{ item.state.department.institute.name }}
        </ng-container>
        <ng-container
          *ngSwitchCase="ReportsTableRowItemField.DEPARTMENT">{{ item.state.department.name }}
        </ng-container>
        <ng-container *ngSwitchCase="ReportsTableRowItemField.HOURS">{{ item.state.hours }}</ng-container>
        <ng-container
          *ngSwitchCase="ReportsTableRowItemField.START_DATE">{{ item.state.startDate }}
        </ng-container>
        <ng-container *ngSwitchCase="ReportsTableRowItemField.END_DATE">{{ item.state.endDate }}</ng-container>
      </ng-container>
    </td>
  `
})
export class ReportsTableRowComponent extends TableRowController<IReport, ReportsTableRowItemField> {
  protected readonly ReportsTableRowItemField = ReportsTableRowItemField;
}
