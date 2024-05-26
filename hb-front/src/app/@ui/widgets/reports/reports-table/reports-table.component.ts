import {ITableConfig, TableController} from "@core/controllers";
import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IPagination, IReportListItem, ITableColumn, ReportStatus} from "@core/models";
import {Component, input, output} from "@angular/core";
import {
  PaginatorComponent,
  ReportsTableRowComponent,
  ReportsTableRowItemField,
  SpinnerComponent,
  TableComponent
} from "@ui/widgets";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {getDefaultPaginationRequest} from "@core/utils";

@Component({
  selector: 'app-reports-table',
  standalone: true,
  imports: [
    TableComponent,
    NgForOf,
    NgIf,
    AsyncPipe,
    ReportsTableRowComponent,
    PaginatorComponent,
    SpinnerComponent,
  ],
  template: `
    <ng-container *ngIf="items$ | async as items">
      <table app-table [cols]="defaultCols" [shadowed]="true" [itemsCount]="items.length">
        <tr *ngFor="let item of items"
            (click)="edit.emit({ id: item.id, status: item.status })"
            app-reports-table-row
            [item]="item"
            [cols]="defaultCols"
        ></tr>
      </table>

      <app-paginator *ngIf="page$ | async as page" [page]="page"></app-paginator>
    </ng-container>
  `
})
export class ReportsTableComponent extends TableController<IReportListItem> {
  public readonly loadFn = input.required<(req: IPaginationRequest) => Observable<IPagination<IReportListItem>>>();

  public readonly edit = output<{ id: string, status: ReportStatus }>();

  protected readonly defaultCols = defaultCols;

  protected config(): ITableConfig {
    return {
      request: getDefaultPaginationRequest()
    };
  }

  protected load(request: IPaginationRequest): Observable<IPagination<IReportListItem>> {
    return this.loadFn()(request);
  }
}

const defaultCols: ITableColumn<ReportsTableRowItemField>[] = [
  {
    id: ReportsTableRowItemField.JOB,
    text: 'Должность',
    order: 1
  },
  {
    id: ReportsTableRowItemField.RATE,
    text: 'Ставка',
    order: 2,
  },
  {
    id: ReportsTableRowItemField.STATUS,
    text: 'Статус',
    order: 3,
  },
  {
    id: ReportsTableRowItemField.DEPARTMENT,
    text: 'Кафедра',
    order: 4
  },
  {
    id: ReportsTableRowItemField.INSTITUTE,
    text: 'Институт',
    order: 5
  },
  {
    id: ReportsTableRowItemField.HOURS,
    text: 'Часы',
    order: 6
  },
  {
    id: ReportsTableRowItemField.START_DATE,
    text: "Дата начала",
    order: 7
  },
  {
    id: ReportsTableRowItemField.END_DATE,
    text: "Дата окончания",
    order: 8
  }
];
