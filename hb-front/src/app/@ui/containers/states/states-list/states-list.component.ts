import {Component} from "@angular/core";
import {ContentComponent, SpinnerComponent, StatesTableComponent, StatesTableHeaderComponent} from "@ui/widgets";
import {StatesDialogService, StatesService} from "@core/abstracts";
import {IPaginationRequest} from "@core/dtos";
import {SubscriptionController} from "@core/controllers";
import {exists, Spinner, switchMapSpinner, withSpinner} from "@core/utils";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-states-list',
  standalone: true,
  imports: [
    ContentComponent,
    StatesTableComponent,
    StatesTableHeaderComponent,
    NgIf,
    SpinnerComponent
  ],
  template: `
    <app-content>
      <app-states-table-header
        (create)="create()"
      ></app-states-table-header>

      <app-states-table [loadFn]="loadFn"></app-states-table>

      <app-spinner *ngIf="spinner.active$"></app-spinner>
    </app-content>
  `,
  host: {class: 'host-class'}
})
export class StatesListComponent extends SubscriptionController {
  constructor(
    private readonly _statesService: StatesService,
    private readonly _statesDialogService: StatesDialogService
  ) {
    super();
  }

  protected readonly spinner = new Spinner();

  protected readonly loadFn = (request: IPaginationRequest) => withSpinner(this._statesService.search(request), this.spinner);

  protected create(): void {
    const ref = this._statesDialogService.openCreate();

    this.subscription.add(
      ref.afterClosed().pipe(
        exists(),
        switchMapSpinner((req) => this._statesService.create(req), this.spinner)
      ).subscribe()
    );
  }
}
