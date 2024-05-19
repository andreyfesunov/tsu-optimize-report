import {Component} from "@angular/core";
import {ActivitiesTableComponent, ContentComponent, SpinnerComponent} from "@ui/widgets";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {exists, Spinner, withSpinner} from "@core/utils";
import {IPaginationRequest} from "@core/dtos";
import {ActivitiesDialogService, ActivitiesService, EventTypesService} from "@core/abstracts";
import {MatIconButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {SubscriptionController} from "@core/controllers";
import {switchMap} from "rxjs";

@Component({
  selector: 'app-activities-list',
  standalone: true,
  template: `
    <app-content>
      <ng-container *ngIf="activities$ | async as activities">
        <ng-container *ngFor="let activity of activities">
          <div class="activities-list-title">
            {{ activity.name }}

            <button mat-icon-button (click)="assign(activity.id, activity.name)">
              <mat-icon>add_circle</mat-icon>
            </button>
          </div>

          <app-activities-table
            [activityId]="activity.id"
            [loadFn]="loadFn"
          ></app-activities-table>
        </ng-container>
      </ng-container>

      <app-spinner *ngIf="spinner.active$"></app-spinner>
    </app-content>
  `,
  imports: [
    ContentComponent,
    SpinnerComponent,
    NgIf,
    AsyncPipe,
    NgForOf,
    ActivitiesTableComponent,
    MatIconButton,
    MatIcon
  ],
  styles: [`
    @use "../../../../../scss/shared/variables";

    .activities-list-title {
      display: flex;
      align-items: center;

      gap: variables.$padding-md;
    }
  `],
  host: {class: 'host-class'}
})
export class ActivitiesListComponent extends SubscriptionController {
  constructor(
    private readonly _activitiesService: ActivitiesService,
    private readonly _eventTypesService: EventTypesService,
    private readonly _activitiesDialogService: ActivitiesDialogService
  ) {
    super();
  }

  protected readonly activities$ = this._activitiesService.getAll();

  protected readonly spinner = new Spinner();

  protected readonly loadFn = (id: string, req: IPaginationRequest) => withSpinner(this._eventTypesService.search(id, req), this.spinner);

  protected assign(id: string, name: string): void {
    const dialog = this._activitiesDialogService.openAddEvent(id, name);

    this.subscription.add(
      dialog.afterClosed().pipe(
        exists(),
        switchMap((req) => this._eventTypesService.assign(req))
      ).subscribe()
    );
  }
}
