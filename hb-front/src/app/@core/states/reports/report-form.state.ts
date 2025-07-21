import { ReportsService, WorksService, RecordsService } from '@core/services';
import { combineLatest, map, Observable, shareReplay } from 'rxjs';
import { IWork, SemesterEnum } from '@core/models';
import { WorkFormStateFactory } from '@core/factories';
import { DestroyRef } from '@angular/core';
import { Spinner, withSpinner } from '@core/utils';

export class ReportFormState {
  public readonly records$ = combineLatest(
    [SemesterEnum.AUTUMN, SemesterEnum.SPRING].map((semesterId) =>
      withSpinner(
        this._recordsService.get(this._id, semesterId),
        this.spinner,
      ).pipe(map((records) => ({ semesterId, records }))),
    ),
  ).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true,
    }),
  );

  public readonly works$: Observable<IWork[]> = withSpinner(
    this._worksService.getAll(),
    this.spinner,
  ).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true,
    }),
  );

  public readonly reports$ = combineLatest(
    [SemesterEnum.AUTUMN, SemesterEnum.SPRING].map((semesterId) =>
      withSpinner(
        this._reportsService.detail(this._id, semesterId),
        this.spinner,
      ).pipe(map((report) => ({ semesterId, report }))),
    ),
  ).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true,
    }),
  );

  public readonly states$ = combineLatest([this.reports$, this.works$]).pipe(
    map(([reports, works]) =>
      reports.map(({ semesterId, report }) => ({
        semesterId,
        works: works.map((work, index) =>
          this._workStateFactory.create(
            index,
            report,
            semesterId,
            work,
            this.spinner,
            this._destroyRef,
          ),
        ),
      })),
    ),
    shareReplay({
      bufferSize: 1,
      refCount: true,
    }),
  );

  public constructor(
    private readonly _id: string,
    private readonly _worksService: WorksService,
    private readonly _reportsService: ReportsService,
    private readonly _recordsService: RecordsService,
    private readonly _workStateFactory: WorkFormStateFactory,
    public readonly spinner: Spinner,
    private readonly _destroyRef: DestroyRef,
  ) {}
}
