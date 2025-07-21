import { Component, DestroyRef, inject, signal } from '@angular/core';
import {
  ContentComponent,
  ReportFormComponent,
  ReportsFirstHalfTableComponent,
  ScrollableComponent,
} from '@ui/widgets';
import { ActivatedRoute } from '@angular/router';
import {
  combineLatest,
  map,
  of,
  shareReplay,
  startWith,
  switchMap,
} from 'rxjs';
import { ParamsRoutes } from '@core/models';
import { exists, Spinner } from '@core/utils';
import { AsyncPipe, NgIf } from '@angular/common';
import { MatButton } from '@angular/material/button';
import {
  MatButtonToggleGroup,
  MatButtonToggle,
} from '@angular/material/button-toggle';
import { MatIcon } from '@angular/material/icon';
import { ReportsService } from '@core/services';
import { SubscriptionController } from '@core/controllers';
import FileSaver from 'file-saver';
import { ReportsFormStateFactory } from '@core/factories';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-report-detail',
  standalone: true,
  imports: [
    ContentComponent,
    ReportFormComponent,
    AsyncPipe,
    NgIf,
    ScrollableComponent,
    ReportsFirstHalfTableComponent,
    MatButton,
    MatIcon,
    MatButtonToggleGroup,
    MatButtonToggle,
  ],
  template: `
    <app-content *ngIf="id$ | async as id" class="report-detail">
      <div class="report-detail-actions">
        <div class="report-detail-actions__left">
          <button mat-flat-button (click)="export(id)">
            <mat-icon>download</mat-icon>
            <span>Скачать</span>
          </button>

          <mat-button-toggle-group [(value)]="semesterId">
            <mat-button-toggle [value]="1">Осень</mat-button-toggle>
            <mat-button-toggle [value]="2">Весна</mat-button-toggle>
          </mat-button-toggle-group>
        </div>

        <div class="report-hours-counter">
          Осталось заполнить часов: План - {{ (plan$ | async) ?? '...' }}, Факт
          - {{ (fact$ | async) ?? '...' }}
        </div>
      </div>

      <div class="report-detail-tabs host-class">
        <app-scrollable
          class="host-class report-table"
          [horizontalScroll]="true"
        >
          <app-scrollable class="host-class" [horizontalScroll]="true">
            <app-reports-first-half-table
              [reportId]="id"
              [semesterId]="semesterId()"
            />
          </app-scrollable>
        </app-scrollable>

        <app-scrollable
          class="host-class report-table"
          [horizontalScroll]="true"
        >
          <app-report-form *ngIf="state$ | async as state" [state]="state" />
        </app-scrollable>
      </div>
    </app-content>
  `,
  host: { class: 'host-class' },
  styleUrls: ['report-detail.component.scss'],
})
export class ReportDetailComponent extends SubscriptionController {
  private readonly _route = inject(ActivatedRoute);
  private readonly _reportsService = inject(ReportsService);
  private readonly _reportStateFactory = inject(ReportsFormStateFactory);
  private readonly _destroyRef = inject(DestroyRef);

  protected readonly semesterId = signal(1);
  protected readonly spinner = new Spinner();

  protected readonly id$ = this._route.paramMap.pipe(
    map((v) => v.get(ParamsRoutes.ID)),
    exists(),
  );

  protected readonly state$ = this.id$.pipe(
    map((id) =>
      this._reportStateFactory.create(id, this.spinner, this._destroyRef),
    ),
    shareReplay({
      bufferSize: 1,
      refCount: true,
    }),
  );

  protected readonly plan$ = this.state$.pipe(
    switchMap((state) =>
      combineLatest([
        state.report$,
        state.states$.pipe(
          map((states) => states.map((x) => x.states$)),
          switchMap((states) => combineLatest(states)),
          map((states) => states.reduce((acc, cur) => [...acc, ...cur], [])),
          switchMap((states) =>
            combineLatest([
              ...states.map((x) =>
                x.lessonFormStates$.pipe(
                  switchMap((x) => {
                    if (x.length === 0) {
                      return of([]);
                    }

                    return combineLatest(
                      x.map((value) =>
                        value.form.controls.plan.valueChanges.pipe(
                          startWith(value.form.controls.plan.value),
                        ),
                      ),
                    );
                  }),
                ),
              ),
              ...states.map((x) =>
                x.commentFormStates$.pipe(
                  switchMap((x) => {
                    if (x.length === 0) {
                      return of([]);
                    }

                    return combineLatest(
                      x.map((value) =>
                        value.form.controls.plan.valueChanges.pipe(
                          startWith(value.form.controls.plan.value),
                        ),
                      ),
                    );
                  }),
                ),
              ),
            ]),
          ),
          map((v) =>
            v
              .reduce((acc, cur) => [...acc, ...cur], [])
              .map((x) => x ?? 0)
              .reduce((acc, cur) => acc + cur, 0),
          ),
        ),
      ]),
    ),
    map(([report, plans]) => report.state.hours - plans),
  );

  protected readonly fact$ = this.state$.pipe(
    switchMap((state) =>
      combineLatest([
        state.report$,
        state.states$.pipe(
          map((states) => states.map((x) => x.states$)),
          switchMap((states) => combineLatest(states)),
          map((states) => states.reduce((acc, cur) => [...acc, ...cur], [])),
          switchMap((states) =>
            combineLatest([
              ...states.map((x) =>
                x.lessonFormStates$.pipe(
                  switchMap((x) => {
                    if (x.length === 0) {
                      return of([]);
                    }

                    return combineLatest(
                      x.map((value) =>
                        value.form.controls.fact.valueChanges.pipe(
                          startWith(value.form.controls.fact.value),
                        ),
                      ),
                    );
                  }),
                ),
              ),
              ...states.map((x) =>
                x.commentFormStates$.pipe(
                  switchMap((x) => {
                    if (x.length === 0) {
                      return of([]);
                    }

                    return combineLatest(
                      x.map((value) =>
                        value.form.controls.fact.valueChanges.pipe(
                          startWith(value.form.controls.fact.value),
                        ),
                      ),
                    );
                  }),
                ),
              ),
            ]),
          ),
          map((v) =>
            v
              .reduce((acc, cur) => [...acc, ...cur], [])
              .map((x) => x ?? 0)
              .reduce((acc, cur) => acc + cur, 0),
          ),
        ),
      ]),
    ),
    map(([report, facts]) => report.state.hours - facts),
  );

  protected export(id: string): void {
    this.subscription.add(
      this._reportsService.export(id).subscribe((data) => {
        FileSaver.saveAs(new Blob([data]), 'ind-plan.xlsx');
      }),
    );
  }
}
