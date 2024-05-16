import {Component} from "@angular/core";
import {ContentComponent, ReportsTableComponent, SpinnerComponent} from "@ui/widgets";
import {ReportsService} from "@core/abstracts";
import {IPaginationRequest} from "@core/dtos";
import {Spinner, withSpinner} from "@core/utils";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-reports-list',
  standalone: true,
  imports: [
    ReportsTableComponent,
    ContentComponent,
    NgIf,
    SpinnerComponent
  ],
  template: `
    <app-content>
      <app-reports-table [loadFn]="loadFn"></app-reports-table>

      <app-spinner *ngIf="spinner.active$"></app-spinner>
    </app-content>
  `,
  host: {class: 'host-class'}
})
export class ReportsListComponent {
  constructor(
    private readonly _reportService: ReportsService
  ) {
  }

  protected readonly spinner = new Spinner();

  protected readonly loadFn = (req: IPaginationRequest) => withSpinner(this._reportService.search(req), this.spinner);
}
