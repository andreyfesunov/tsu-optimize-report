import { Component, input, ViewEncapsulation } from '@angular/core';
import { MatTab, MatTabContent, MatTabGroup } from '@angular/material/tabs';
import {
  BehaviorSubject,
  combineLatest,
  map,
  shareReplay,
  switchMap,
} from 'rxjs';
import { AsyncPipe, NgForOf, NgIf } from '@angular/common';
import { SpinnerComponent, WorkFormComponent } from '@ui/widgets';
import { SubscriptionController } from '@core/controllers';
import { ReportFormState } from '@core/states';
import { SemesterEnum } from '@core/models';
import { toObservable } from '@angular/core/rxjs-interop';

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
    MatTabContent,
  ],
  template: `
    <mat-tab-group
      *ngIf="workStates$ | async as workStates"
      class="reports-form host-class"
      animationDuration="0ms"
      (selectedTabChange)="tabChanged$.next($event.index)"
    >
      <mat-tab
        *ngFor="let workState of workStates; index as index"
        [label]="workState.work.name"
      >
        <ng-template matTabContent>
          <app-report-work-form
            [state]="workState"
            [workIndex]="index"
          ></app-report-work-form>
        </ng-template>
      </mat-tab>
    </mat-tab-group>

    <app-spinner *ngIf="state().spinner.active$"></app-spinner>
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
    `,
  ],
  host: { class: 'host-class' },
})
export class ReportFormComponent extends SubscriptionController {
  public readonly semesterId = input.required<SemesterEnum>();
  public readonly state = input.required<ReportFormState>();

  private readonly _state$ = toObservable(this.state);
  private readonly _semesterId$ = toObservable(this.semesterId);

  protected readonly workStates$ = combineLatest([
    this._state$,
    this._semesterId$,
  ]).pipe(
    switchMap(([state, semesterId]) =>
      state.states$.pipe(
        map((states) =>
          states
            .filter((x) => x.semesterId === semesterId)
            .map((x) => x.works)
            .reduce((acc, cur) => [...acc, ...cur], []),
        ),
      ),
    ),
    shareReplay({ bufferSize: 1, refCount: true }),
  );

  protected readonly tabChanged$: BehaviorSubject<number> =
    new BehaviorSubject<number>(0);
}
