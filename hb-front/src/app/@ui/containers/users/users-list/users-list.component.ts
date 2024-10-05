import {Component, inject} from "@angular/core";
import {ContentComponent, SpinnerComponent, UsersTableComponent} from "@ui/widgets";
import {IPaginationRequest} from "@core/dtos";
import {NgIf} from "@angular/common";
import {Spinner, withSpinner} from "@core/utils";
import {UsersService} from "@core/services";

@Component({
  selector: 'app-users-list',
  standalone: true,
  template: `
    <app-content>
      <app-users-table [loadFn]="loadFn"></app-users-table>

      <app-spinner *ngIf="spinner.active$"></app-spinner>
    </app-content>
  `,
  imports: [
    ContentComponent,
    UsersTableComponent,
    NgIf,
    SpinnerComponent
  ],
  host: {class: 'host-class'}
})
export class UsersListComponent {
  private readonly _usersService = inject(UsersService);

  protected readonly spinner = new Spinner();

  protected readonly loadFn = (request: IPaginationRequest) => withSpinner(this._usersService.search(request), this.spinner);
}
