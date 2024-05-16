import {Component} from "@angular/core";
import {ContentComponent, StatesTableComponent, StatesTableHeaderComponent} from "@ui/widgets";
import {StatesDialogService, StatesService} from "@core/abstracts";
import {IPaginationRequest} from "@core/dtos";
import {SubscriptionController} from "@core/controllers";
import {exists} from "@core/utils";

@Component({
  selector: 'app-states-list',
  standalone: true,
  imports: [
    ContentComponent,
    StatesTableComponent,
    StatesTableHeaderComponent
  ],
  template: `
    <app-content>
      <app-states-table-header
        (create)="create()"
      ></app-states-table-header>
      <app-states-table [loadFn]="loadFn"></app-states-table>
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

  protected create(): void {
    const ref = this._statesDialogService.openCreate();

    this.subscription.add(
      ref.afterClosed().pipe(
        exists()
      ).subscribe((req) => this._statesService.create(req))
    );
  }

  protected readonly loadFn = (request: IPaginationRequest) => this._statesService.search(request);
}
