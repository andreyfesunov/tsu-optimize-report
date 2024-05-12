import {Component} from "@angular/core";
import {ContentComponent, UsersTableComponent} from "@ui/widgets";
import {UsersService} from "@core/abstracts";
import {IPaginationRequest} from "@core/dtos";

@Component({
  selector: 'app-users-list',
  standalone: true,
  template: `
    <app-content>
      <app-users-table [loadFn]="loadFn"></app-users-table>
    </app-content>
  `,
  imports: [
    ContentComponent,
    UsersTableComponent
  ],
  host: {class: 'host-class'}
})
export class UsersListComponent {
  constructor(
    private readonly _usersService: UsersService
  ) {
  }

  protected readonly loadFn = (request: IPaginationRequest) => this._usersService.search(request);
}
