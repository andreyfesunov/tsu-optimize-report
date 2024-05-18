import {ITableConfig, TableController} from "@core/controllers";
import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IPagination, IReport, ITableColumn} from "@core/models";
import {Component, input} from "@angular/core";
import {
  PaginatorComponent,
  ReportsTableRowComponent,
  ReportsTableRowItemField,
  SpinnerComponent,
  TableComponent
} from "@ui/widgets";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {getDefaultPaginationRequest} from "@core/utils";
import {ReportsDialogService} from "@core/abstracts";

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
      <table app-table [cols]="defaultCols" [shadowed]="true" *ngIf="items.length > 0">
        <tr *ngFor="let item of items"
            (onclick)="edit(item.id)"
            app-reports-table-row
            [item]="item"
            [cols]="defaultCols"
        ></tr>
      </table>

      <app-paginator *ngIf="page$ | async as page" [page]="page"></app-paginator>
    </ng-container>
  `
})
export class ReportsTableComponent extends TableController<IReport> {
  constructor(
    private readonly _reportsDialogService: ReportsDialogService
  ) {
    super();
  }

  public readonly loadFn = input.required<(req: IPaginationRequest) => Observable<IPagination<IReport>>>();

  protected readonly defaultCols = defaultCols;

  protected config(): ITableConfig {
    return {
      request: getDefaultPaginationRequest()
    };
  }

  protected load(request: IPaginationRequest): Observable<IPagination<IReport>> {
    return this.loadFn()(request);
  }

  protected edit(id: string): void {
    console.log(this, id);
    this._reportsDialogService.openDetail(id);
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
    id: ReportsTableRowItemField.DEPARTMENT,
    text: 'Кафедра',
    order: 3
  },
  {
    id: ReportsTableRowItemField.INSTITUTE,
    text: 'Институт',
    order: 4
  },
  {
    id: ReportsTableRowItemField.HOURS,
    text: 'Часы',
    order: 5
  },
  {
    id: ReportsTableRowItemField.START_DATE,
    text: "Дата начала",
    order: 6
  },
  {
    id: ReportsTableRowItemField.END_DATE,
    text: "Дата окончания",
    order: 7
  }
];
