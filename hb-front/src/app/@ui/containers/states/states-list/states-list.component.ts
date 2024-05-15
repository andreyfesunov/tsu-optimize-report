import {Component} from "@angular/core";
import {ContentComponent, StatesTableComponent, StatesTableHeaderComponent} from "@ui/widgets";
import {StatesDialogService, StatesService} from "@core/abstracts";
import {IPaginationRequest} from "@core/dtos";

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
export class StatesListComponent {
  constructor(
    private readonly _statesService: StatesService,
    private readonly _statesDialogService: StatesDialogService
  ) {
  }

  protected create(): void {
    this._statesDialogService.openCreate();
  }

  protected readonly loadFn = (request: IPaginationRequest) => this._statesService.search(request);
}
