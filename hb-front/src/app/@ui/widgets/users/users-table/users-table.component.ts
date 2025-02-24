import { ITableConfig, TableController } from "@core/controllers";
import { IPagination, ITableColumn, IUserState } from "@core/models";
import { IPaginationRequest } from "@core/dtos";
import { BehaviorSubject, map, Observable, shareReplay, switchMap } from "rxjs";
import { getDefaultPaginationRequest } from "@core/utils";
import { Component, input } from "@angular/core";
import { AsyncPipe, NgForOf, NgIf } from "@angular/common";
import { PaginatorComponent, SpinnerComponent, TableComponent, UsersTableRowComponent } from "@ui/widgets";
import { UsersTableRowItemField } from "@ui/widgets/users/users-table/users-table-row.component";
import { UsersTableStateRowComponent } from "@ui/widgets/users/users-table/users-table-state-row.component";

@Component({
  selector: 'app-users-table',
  standalone: true,
  imports: [
    AsyncPipe,
    NgForOf,
    NgIf,
    PaginatorComponent,
    TableComponent,
    UsersTableRowComponent,
    SpinnerComponent,
    UsersTableStateRowComponent
  ],
  template: `
    <ng-container *ngIf="items$ | async as items">
      <table app-table [cols]="defaultCols" [shadowed]="true" [itemsCount]="items.length">
        <ng-container *ngFor="let item of items">
          <tr app-users-table-row
              [item]="item"
              [cols]="defaultCols">
          </tr>
          <tr *ngIf="item.expanded && item?.states?.length"
              app-users-table-state-row
              [states]="item.states"
              [colsCount]="defaultCols.length">
          </tr>
        </ng-container>
      </table>
      <app-paginator *ngIf="page$ | async as page" [page]="page"></app-paginator>
    </ng-container>
  `
})
export class UsersTableComponent extends TableController<IUserState> {
  protected readonly requestUserStates$: BehaviorSubject<IPaginationRequest> = new BehaviorSubject<IPaginationRequest>(this.config().request);

  protected readonly pageUserStates$: Observable<IPagination<IUserState>> = this.requestUserStates$.pipe(
    switchMap((request) => this.load(request)),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  protected readonly itemsUserStates$: Observable<IUserState[]> = this.pageUserStates$.pipe(map((page) => page.entities));

  public readonly loadFn = input.required<(req: IPaginationRequest) => Observable<IPagination<IUserState>>>();

  protected readonly defaultCols = defaultCols;

  protected config(): ITableConfig {
    return {
      request: getDefaultPaginationRequest()
    };
  }

  protected load(request: IPaginationRequest): Observable<IPagination<IUserState>> {
    return this.loadFn()(request);
  }
}

const defaultCols: ITableColumn<UsersTableRowItemField>[] = [
  {
    id: UsersTableRowItemField.EXPAND,
    text: '',
    order: 0,
    align: 'center'
  },
  {
    id: UsersTableRowItemField.EMAIL,
    text: 'Email',
    order: 1
  },
  {
    id: UsersTableRowItemField.FIRSTNAME,
    text: 'Имя',
    order: 2
  },
  {
    id: UsersTableRowItemField.LASTNAME,
    text: 'Фамилия',
    order: 3
  }
]
