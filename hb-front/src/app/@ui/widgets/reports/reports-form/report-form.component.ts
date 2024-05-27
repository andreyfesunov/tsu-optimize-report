import {Component, computed, DestroyRef, input} from '@angular/core';
import {MatTab, MatTabGroup} from '@angular/material/tabs';
import {BehaviorSubject} from "rxjs";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {SpinnerComponent, WorkFormComponent} from "@ui/widgets";
import {Spinner} from "@core/utils";
import {SubscriptionController} from "@core/controllers";
import {ReportsFormStateFactory} from "@core/factories";

@Component({
  selector: 'app-report-form',
  standalone: true,
  imports: [
    MatTabGroup,
    NgIf,
    NgForOf,
    MatTab,
    SpinnerComponent,
    AsyncPipe,
    WorkFormComponent

  ],
  template: `
    <mat-tab-group *ngIf="state().states$ | async as workStates" class="reports-form" animationDuration="0ms"
                   (selectedTabChange)="tabChanged$.next($event.index)">
      <mat-tab *ngFor="let workState of workStates; index as index" [disabled]="index !== 0"
               [label]="workState.work.name">
        <app-report-work-form [state]="workState"></app-report-work-form>
      </mat-tab>
    </mat-tab-group>

    <app-spinner *ngIf="spinner.active$"></app-spinner>
  `,
  styles: [
    `
      .reports-form {
        background-color: white;
      }

      h1 {
        margin: 0;
      }
    `
  ]
})
export class ReportFormComponent extends SubscriptionController {
  constructor(
    private readonly _reportStateFactory: ReportsFormStateFactory,
    private readonly _destroyRef: DestroyRef
  ) {
    super();
  }

  public readonly id = input.required<string>();

  protected readonly state = computed(() => this._reportStateFactory.create(this.id(), this._destroyRef));

  protected readonly spinner = new Spinner();

  protected readonly tabChanged$: BehaviorSubject<number> = new BehaviorSubject<number>(0);
}

