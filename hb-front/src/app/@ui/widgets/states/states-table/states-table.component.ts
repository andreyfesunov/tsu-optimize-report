import {ITableConfig, TableController} from "@core/controllers";
import {IPagination, IState, ITableColumn} from "@core/models";
import {Component, input} from "@angular/core";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {
  PaginatorComponent,
  SpinnerComponent,
  StatesTableRowComponent,
  StateTableRowItemField,
  TableComponent
} from "@ui/widgets";
import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {getDefaultPaginationRequest} from "@core/utils";

@Component({
  selector: 'app-states-table',
  standalone: true,
  template: `
    <ng-container *ngIf="items$ | async as items">
      <table app-table [shadowed]="true" [cols]="defaultCols" [itemsCount]="items.length">
        <tr *ngFor="let item of items" app-states-table-row [item]="item" [cols]="defaultCols"></tr>
      </table>

      <app-paginator *ngIf="page$ | async as page" [page]="page"></app-paginator>
    </ng-container>
  `,
  imports: [
    AsyncPipe,
    TableComponent,
    NgIf,
    PaginatorComponent,
    StatesTableRowComponent,
    NgForOf,
    SpinnerComponent
  ]
})
export class StatesTableComponent extends TableController<IState> {
  public readonly loadFn = input.required<(request: IPaginationRequest) => Observable<IPagination<IState>>>();

  protected readonly defaultCols = defaultCols;

  protected config(): ITableConfig {
    return {
      request: getDefaultPaginationRequest()
    };
  }

  protected load(request: IPaginationRequest): Observable<IPagination<IState>> {
    return this.loadFn()(request);
  }
}

const defaultCols: ITableColumn<StateTableRowItemField>[] = [
  {
    id: StateTableRowItemField.JOB,
    text: 'Должность',
    order: 1
  },
  {
    id: StateTableRowItemField.COUNT,
    text: 'Количество доступных',
    order: 2
  },
  {
    id: StateTableRowItemField.HOURS,
    text: 'Рабочие часы',
    order: 3,
  },
  {
    id: StateTableRowItemField.START_DATE,
    text: 'Дата начала',
    order: 4
  },
  {
    id: StateTableRowItemField.END_DATE,
    text: 'Дата окончания',
    order: 5
  }
]
