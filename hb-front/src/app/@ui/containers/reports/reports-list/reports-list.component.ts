import {Component} from "@angular/core";
import {ContentComponent, ReportsTableComponent, ReportsTabsComponent} from "@ui/widgets";
import {ReportsService} from "@core/abstracts";
import {IPaginationRequest} from "@core/dtos";

@Component({
  selector: 'app-reports-list',
  standalone: true,
  imports: [
    ReportsTabsComponent,
    ReportsTableComponent,
    ContentComponent
  ],
  template: `
    <app-content>
      <app-reports-table [loadFn]="loadFn"></app-reports-table>
    </app-content>
  `,
  host: {class: 'host-class'}
})
export class ReportsListComponent {
  constructor(
    private readonly _reportService: ReportsService
  ) {
  }

  protected readonly loadFn = (req: IPaginationRequest) => this._reportService.search(req);
}
