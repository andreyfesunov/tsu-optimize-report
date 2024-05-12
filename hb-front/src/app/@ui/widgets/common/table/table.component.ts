import {ChangeDetectionStrategy, Component, Input, ViewEncapsulation} from "@angular/core";
import {ITableColumn} from "@core/models";
import {NgForOf, NgStyle} from "@angular/common";
import {ArraySortPipe} from "@core/pipes";

@Component({
    selector: 'table[app-table]',
    standalone: true,
    template: `
        <thead class="tsu-table__thead" [class.tsu-table__thead--sticky]="sticky">
        <tr>
            <th
                    *ngFor="let col of cols | sort : 'order'"
                    class="tsu-table-th"
                    [ngStyle]="{ width: col.width }"
            >
                <div class="tsu-table-th-item"
                     [class.tsu-table-th-item__center]="col.align === 'center'">{{ col.text }}</div>
            </th>
        </tr>
        </thead>
        <tbody class="tsu-table__tbody">
        <ng-content></ng-content>
        </tbody>
    `,
    encapsulation: ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        NgForOf,
        ArraySortPipe,
        NgStyle
    ],
    host: {
        class: 'tsu-table',
        '[class.tsu-table--mini]': 'mini',
        '[class.tsu-table--shadowed]': 'shadowed',
        '[class.tsu-table--bordered]': 'bordered',
    }
})
export class TableComponent {
    @Input() public mini: boolean = false;
    @Input() public shadowed: boolean = false;
    @Input() public bordered: boolean = false;
    @Input() public sticky: boolean = false;

    @Input({required: true}) public cols: ITableColumn[] = [];
}
