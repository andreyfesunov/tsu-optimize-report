import {Component, computed, DestroyRef, input, ViewEncapsulation} from '@angular/core';
import {MatTab, MatTabContent, MatTabGroup} from '@angular/material/tabs';
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
    WorkFormComponent,
    MatTabContent
  ],
  template: `
    <mat-tab-group *ngIf="state().states$ | async as workStates" class="reports-form host-class" animationDuration="0ms"
                   (selectedTabChange)="tabChanged$.next($event.index)">
      <mat-tab *ngFor="let workState of workStates; index as index" [disabled]="index !== 0"
               [label]="workState.work.name">
        <ng-template matTabContent>
          <app-report-work-form
            [state]="workState"
            [index]="index"
          ></app-report-work-form>
        </ng-template>
      </mat-tab>
    </mat-tab-group>

    <app-spinner *ngIf="spinner.active$"></app-spinner>
  `,
  encapsulation: ViewEncapsulation.None,
  styles: [
    `
      mat-tab-group.reports-form {
        > div,
        mat-tab-body > div {
          display: flex;
          flex-direction: column;
          flex: 1 1 1px;
        }
      }

      h1 {
        margin: 0;
      }
    `
  ],
  host: {class: 'host-class'}
})
export class ReportFormComponent extends SubscriptionController {
  constructor(
    private readonly _reportStateFactory: ReportsFormStateFactory,
    private readonly _destroyRef: DestroyRef
  ) {
    super();
  }

  public readonly id = input.required<string>();

  protected readonly spinner = new Spinner();

  protected readonly state = computed(() => this._reportStateFactory.create(
    this.id(),
    this.spinner,
    this._destroyRef
  ));

  protected readonly tabChanged$: BehaviorSubject<number> = new BehaviorSubject<number>(0);
}

