import {Component, input} from "@angular/core";
import {WorkFormState} from "@core/states";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {MatButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {EventFormComponent, ScrollableComponent} from "@ui/widgets";

@Component({
  selector: "app-report-work-form",
  standalone: true,
  imports: [
    ScrollableComponent,
    NgIf,
    EventFormComponent,
    MatButton,
    MatIcon,
    AsyncPipe,
    NgForOf

  ],
  template: `
    <app-scrollable class="work-form__scrollable" [horizontalScroll]="true">
      <div *ngIf="state().states$ | async as eventStates" class="work-form__content">
        <app-report-event-form
          *ngFor="let eventState of eventStates; index as index"
          [state]="eventState"
          [workIndex]="workIndex()"
          (deleteState)="state().deleteState(index)"
        ></app-report-event-form>

        <button mat-button (click)="state().addEvent()" [disabled]="state().addStateDisabled$ | async">
          <mat-icon>add_circle</mat-icon>
          Добавить событие
        </button>
      </div>
    </app-scrollable>
  `,
  styles: [
    `
      @use "../../../../../scss/shared/variables";

      .work-form {
        &__scrollable {
          background-color: variables.$table-background-color;
        }

        &__content {
          padding: variables.$padding-lg;

          display: flex;
          flex-direction: column;
          gap: variables.$padding-lg;
        }
      }
    `
  ],
  host: {class: 'host-class'}
})
export class WorkFormComponent {
  public readonly state = input.required<WorkFormState>();
  public readonly workIndex = input.required<number>();
}

