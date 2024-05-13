import {Component} from "@angular/core";
import {ContentComponent, StatesTableComponent} from "@ui/widgets";
import {StatesService} from "@core/abstracts";
import {IPaginationRequest} from "@core/dtos";

@Component({
  selector: 'app-states-list',
  standalone: true,
  imports: [
    ContentComponent,
    StatesTableComponent
  ],
  template: `
    <app-content>
      <app-states-table [loadFn]="loadFn"></app-states-table>
    </app-content>
  `,
  host: {class: 'host-class'}
})
export class StatesListComponent {
  constructor(private readonly _statesService: StatesService) {
  }

  protected readonly loadFn = (request: IPaginationRequest) => this._statesService.search(request);
}
