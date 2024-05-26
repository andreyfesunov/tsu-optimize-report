import {ReportsService, WorksService} from "@core/services";
import {combineLatest, map, Observable, shareReplay} from "rxjs";
import {IReportDetail, IWork} from "@core/models";
import {WorkFormState} from "@core/states";
import {WorkFormStateFactory} from "@core/factories";
import {DestroyRef} from "@angular/core";

export class ReportFormState {
  public constructor(
    private readonly _id: string,
    private readonly _worksService: WorksService,
    private readonly _reportsService: ReportsService,
    private readonly _workStateFactory: WorkFormStateFactory,
    private readonly _destroyRef: DestroyRef
  ) {
  }

  private readonly _report$: Observable<IReportDetail> = this._reportsService.detail(this._id).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  )

  public readonly works$: Observable<IWork[]> = this._worksService.getAll().pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  public readonly states$: Observable<WorkFormState[]> = combineLatest([this._report$, this.works$]).pipe(
    map(([report, works]) => works.map((work) => this._workStateFactory.create(
      report,
      work,
      this._destroyRef
    ))),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );
}
