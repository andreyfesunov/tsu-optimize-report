import {Component, inject, input} from "@angular/core";
import {ITableConfig, TableController} from "@core/controllers";
import {IActivity, IPagination, IRecord, ITableColumn} from "@core/models";
import {map, Observable} from "rxjs";
import {getDefaultPaginationRequest} from "@core/utils";
import {RecordsService} from "@core/services/features/records.service";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {
  FirstHalfItemField,
  IRecordEntry,
  ReportsFirstHalfTableRowComponent,
  ScrollableComponent,
  TableComponent
} from "@ui/widgets";

@Component({
  selector: 'app-reports-first-half-table',
  standalone: true,
  imports: [
    AsyncPipe,
    NgIf,
    TableComponent,
    ReportsFirstHalfTableRowComponent,
    NgForOf,
    ScrollableComponent
  ],
  template:
    `
      <ng-container *ngIf="items$ | async as items">
        <table app-table [cols]="getCols(items)" [shadowed]="true">
          <tr *ngFor="let item of items"
              app-reports-first-half-table-row
              [cols]="getCols(items)"
              [item]="item"
              [clickable]="false"
          ></tr>
        </table>
      </ng-container>
    `,
  host: {class: 'host-class'}
})
export class ReportsFirstHalfTableComponent extends TableController<IRecordEntry> {
  private readonly _service = inject(RecordsService);

  public readonly reportId = input.required<string>();

  protected config(): ITableConfig {
    return {request: getDefaultPaginationRequest()};
  }

  protected load(): Observable<IPagination<IRecordEntry>> {
    return this._service.get(this.reportId()).pipe(
      map((records) => this._toEntry(records)),
      map((records) => ({
        entities: records,
        pageSize: 9999,
        pageNumber: 1,
        totalPages: 1
      }))
    );
  }

  protected getCols(items: IRecordEntry[]): ITableColumn[] {
    const lesson: ITableColumn<FirstHalfItemField> = {
      id: FirstHalfItemField.LESSON,
      order: 0,
      text: "Название предмета"
    };

    const activitiesMap: { [key: string]: string } = {};

    items
      .map((item) => item.activities)
      .reduce((prev, next) => prev.concat(next))
      .forEach((v) => activitiesMap[v.id] = v.name);

    const activities = Object.entries(activitiesMap).map(([id, value], index) => ({
      id: id,
      text: value,
      order: index + 1
    }))

    return [lesson, ...activities]
  }

  private _toEntry(records: IRecord[]): IRecordEntry[] {
    const entries: { [key: string]: (IActivity & { hours: number })[] } = {};

    records.forEach((record) => {
      if (!entries[record.lessonType.name]) entries[record.lessonType.name] = [];
      entries[record.lessonType.name].push({...record.activity, hours: record.hours});
    })

    return Object.entries(entries).map(([name, activities]) => ({
      lessonTypeName: name,
      activities: activities
    }));
  }
}
