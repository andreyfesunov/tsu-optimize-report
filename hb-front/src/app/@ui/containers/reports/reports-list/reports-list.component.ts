import {Component} from "@angular/core";
import {ReportsTabsComponent} from "@ui/widgets";

@Component({
    selector: 'app-reports-list',
    standalone: true,
    imports: [
        ReportsTabsComponent
    ],
    template: `
        <app-reports-tabs></app-reports-tabs>
    `
})
export class ReportsListComponent {

}
