import {ReportsService, WorksService} from "@core/services";
import {combineLatest, map, Observable, shareReplay} from "rxjs";
import {IReportDetail, IWork} from "@core/models";
import {WorkFormState} from "@core/states";
import {WorkFormStateFactory} from "@core/factories";
import {DestroyRef} from "@angular/core";
import {Spinner, withSpinner} from "@core/utils";

export class ReportFormState {
  public constructor(
    private readonly _id: string,
    private readonly _worksService: WorksService,
    private readonly _reportsService: ReportsService,
    private readonly _workStateFactory: WorkFormStateFactory,
    private readonly _spinner: Spinner,
    private readonly _destroyRef: DestroyRef
  ) {
  }

  private readonly _report$: Observable<IReportDetail> = withSpinner(this._reportsService.detail(this._id), this._spinner).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  )

  public readonly works$: Observable<IWork[]> = withSpinner(this._worksService.getAll(), this._spinner).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  public readonly states$: Observable<WorkFormState[]> = combineLatest([this._report$, this.works$]).pipe(
    map(([report, works]) => works.map((work) => this._workStateFactory.create(
      report,
      work,
      this._spinner,
      this._destroyRef
    ))),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );
}
