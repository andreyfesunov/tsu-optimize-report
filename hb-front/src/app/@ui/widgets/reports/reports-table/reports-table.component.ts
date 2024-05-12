import {ITableConfig, TableController} from "@core/controllers";
import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IPagination, IReport, ITableColumn} from "@core/models";
import {ReportService} from "@core/abstracts";
import {Component} from "@angular/core";
import {ReportsTableRowComponent, ReportsTableRowItemField, TableComponent} from "@ui/widgets";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";

@Component({
    selector: 'app-reports-table',
    standalone: true,
    imports: [
        TableComponent,
        NgForOf,
        NgIf,
        AsyncPipe,
        ReportsTableRowComponent,
    ],
    template: `
        <ng-container *ngIf="items$ | async as items">
            <table app-table [cols]="defaultCols" *ngIf="items.length > 0">
                <tr *ngFor="let item of items"
                    app-reports-table-row
                    [item]="item"
                    [cols]="defaultCols"
                ></tr>
            </table>
        </ng-container>
    `
})
export class ReportsTableComponent extends TableController<IReport> {
    constructor(private readonly _reportService: ReportService) {
        super();
    }

    protected readonly defaultCols = defaultCols;

    protected config(): ITableConfig {
        return {
            request: {
                pageNumber: 1,
                pageSize: 25
            }
        };
    }

    // TODO move to container
    protected load(request: IPaginationRequest): Observable<IPagination<IReport>> {
        return this._reportService.search(request);
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
