import {ITableConfig, TableController} from "@core/controllers";
import {IEventType, IPagination, ITableColumn} from "@core/models";
import {Component, input, output} from "@angular/core";
import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {getDefaultPaginationRequest} from "@core/utils";
import {
  ActivitiesTableRowComponent,
  ActivitiesTableRowItemField,
  PaginatorComponent,
  TableComponent
} from "@ui/widgets";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-activities-table',
  standalone: true,
  imports: [
    AsyncPipe,
    NgForOf,
    NgIf,
    PaginatorComponent,
    TableComponent,
    ActivitiesTableRowComponent
  ],
  template: `
    <ng-container *ngIf="items$ | async as items">
      <table app-table [cols]="defaultCols" [shadowed]="true" [itemsCount]="items.length">
        <tr *ngFor="let item of items"
            (click)="edit.emit(item.id)"
            app-activities-table-row
            [item]="item"
            [cols]="defaultCols"
        ></tr>
      </table>

      <app-paginator *ngIf="page$ | async as page" [page]="page"></app-paginator>
    </ng-container>
  `
})
export class ActivitiesTableComponent extends TableController<IEventType> {
  public readonly activityId = input.required<string>();
  public readonly loadFn = input.required<(activityId: string, req: IPaginationRequest) => Observable<IPagination<IEventType>>>();

  public readonly edit = output<string>();

  protected readonly defaultCols = defaultCols;

  protected config(): ITableConfig {
    return {
      request: getDefaultPaginationRequest(2)
    };
  }

  protected load(request: IPaginationRequest): Observable<IPagination<IEventType>> {
    return this.loadFn()(this.activityId(), request);
  }
}

const defaultCols: ITableColumn<ActivitiesTableRowItemField>[] = [
  {
    id: ActivitiesTableRowItemField.NAME,
    order: 1,
    text: 'Название'
  },
  {
    id: ActivitiesTableRowItemField.DESCRIPTION,
    order: 2,
    text: 'Комментарий'
  }
]
