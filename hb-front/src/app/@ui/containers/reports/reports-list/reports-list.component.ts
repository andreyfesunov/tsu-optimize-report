import {Component, inject} from "@angular/core";
import {ContentComponent, ReportsTableComponent, SpinnerComponent} from "@ui/widgets";
import {IPaginationRequest} from "@core/dtos";
import {exists, Spinner, switchMapSpinner, withSpinner} from "@core/utils";
import {NgIf} from "@angular/common";
import {ReportStatus, toReport} from "@core/models";
import {SubscriptionController} from "@core/controllers";
import {ReportsDialogService, ReportsService, RouterService} from "@core/services";

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
      <app-reports-table
        [loadFn]="loadFn"
        (edit)="edit($event)"
      ></app-reports-table>

      <app-spinner *ngIf="spinner.active$"></app-spinner>
    </app-content>
  `,
  host: {class: 'host-class'}
})
export class ReportsListComponent extends SubscriptionController {
  private readonly _reportService = inject(ReportsService);
  private readonly _reportDialogService = inject(ReportsDialogService);
  private readonly _routerService = inject(RouterService);

  protected readonly spinner = new Spinner();

  protected readonly loadFn = (req: IPaginationRequest) => withSpinner(this._reportService.search(req), this.spinner);

  protected edit(data: { id: string, status: ReportStatus }): void {
    data.status === ReportStatus.ACTIVE && this._openDetail(data.id);
    data.status === ReportStatus.NOT_ACTIVE && this._openStart(data.id);
  }

  private _openDetail(id: string): void {
    this._routerService.navigate(toReport(id));
  }

  private _openStart(id: string): void {
    const ref = this._reportDialogService.openStart();

    this.subscription.add(
      ref.afterClosed().pipe(
        exists(),
        switchMapSpinner((data) => this._reportService.create(id, data), this.spinner)
      ).subscribe(() => this._openDetail(id))
    )
  }
}
