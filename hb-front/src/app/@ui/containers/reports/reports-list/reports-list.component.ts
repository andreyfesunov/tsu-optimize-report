import {Component} from "@angular/core";
import {ReportsTableComponent, ReportsTabsComponent} from "@ui/widgets";

@Component({
    selector: 'app-reports-list',
    standalone: true,
    imports: [
        ReportsTabsComponent,
        ReportsTableComponent
    ],
    template: `
        <app-reports-table></app-reports-table>
    `
})
export class ReportsListComponent {

}
