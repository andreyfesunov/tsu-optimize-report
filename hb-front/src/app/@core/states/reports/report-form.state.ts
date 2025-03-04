import {ReportsService, WorksService} from "@core/services";
import {combineLatest, map, Observable, shareReplay} from "rxjs";
import {IReportDetail, IWork} from "@core/models";
import {WorkFormState} from "@core/states";
import {WorkFormStateFactory} from "@core/factories";
import {DestroyRef} from "@angular/core";
import {Spinner, withSpinner} from "@core/utils";

export class ReportFormState {
  public readonly works$: Observable<IWork[]> = withSpinner(this._worksService.getAll(), this.spinner).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );
  public readonly report$: Observable<IReportDetail> = withSpinner(this._reportsService.detail(this._id), this.spinner).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  )
  public readonly states$: Observable<WorkFormState[]> = combineLatest([this.report$, this.works$]).pipe(
    map(([report, works]) => works.map((work, index) => this._workStateFactory.create(
      index,
      report,
      work,
      this.spinner,
      this._destroyRef
    ))),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  public constructor(
    private readonly _id: string,
    private readonly _worksService: WorksService,
    private readonly _reportsService: ReportsService,
    private readonly _workStateFactory: WorkFormStateFactory,
    public readonly spinner: Spinner,
    private readonly _destroyRef: DestroyRef
  ) {
  }
}
