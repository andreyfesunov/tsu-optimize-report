import { DestroyRef, Injectable, inject } from '@angular/core';
import { ReportsService, WorksService, RecordsService } from '@core/services';
import { ReportFormState } from '@core/states';
import { WorkFormStateFactory } from '@core/factories';
import { Spinner } from '@core/utils';

@Injectable({ providedIn: 'root' })
export class ReportsFormStateFactory {
  private readonly _worksService = inject(WorksService);
  private readonly _reportsService = inject(ReportsService);
  private readonly _workStateFactory = inject(WorkFormStateFactory);
  private readonly _recordsService = inject(RecordsService);

  public create(
    id: string,
    spinner: Spinner,
    destroyRef: DestroyRef,
  ): ReportFormState {
    return new ReportFormState(
      id,
      this._worksService,
      this._reportsService,
      this._recordsService,
      this._workStateFactory,
      spinner,
      destroyRef,
    );
  }
}
