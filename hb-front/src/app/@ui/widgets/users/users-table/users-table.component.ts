import {ITableConfig, TableController} from "@core/controllers";
import {IPagination, ITableColumn, IUser} from "@core/models";
import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {getDefaultPaginationRequest} from "@core/utils";
import {Component, input} from "@angular/core";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {PaginatorComponent, TableComponent, UsersTableRowComponent} from "@ui/widgets";
import {UsersTableRowItemField} from "@ui/widgets/users/users-table/users-table-row.component";

@Component({
  selector: 'app-users-table',
  standalone: true,
  imports: [
    AsyncPipe,
    NgForOf,
    NgIf,
    PaginatorComponent,
    TableComponent,
    UsersTableRowComponent
  ],
  template: `
    <ng-container *ngIf="items$ | async as items">
      <table app-table [cols]="defaultCols" [shadowed]="true" *ngIf="items.length > 0">
        <tr *ngFor="let item of items"
            app-users-table-row
            [item]="item"
            [cols]="defaultCols"
        ></tr>
      </table>

      <app-paginator *ngIf="page$ | async as page" [page]="page"></app-paginator>
    </ng-container>
  `
})
export class UsersTableComponent extends TableController<IUser> {
  public readonly loadFn = input.required<(req: IPaginationRequest) => Observable<IPagination<IUser>>>();

  protected readonly defaultCols = defaultCols;

  protected config(): ITableConfig {
    return {
      request: getDefaultPaginationRequest()
    };
  }

  protected load(request: IPaginationRequest): Observable<IPagination<IUser>> {
    return this.loadFn()(request);
  }
}

const defaultCols: ITableColumn<UsersTableRowItemField>[] = [
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
