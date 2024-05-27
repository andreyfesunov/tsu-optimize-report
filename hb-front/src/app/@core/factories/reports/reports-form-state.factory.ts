import {DestroyRef, Injectable} from "@angular/core";
import {ReportsService, WorksService} from "@core/services";
import {ReportFormState} from "@core/states";
import {WorkFormStateFactory} from "@core/factories";
import {Spinner} from "@core/utils";

@Injectable({providedIn: "root"})
export class ReportsFormStateFactory {
  constructor(
    private readonly _worksService: WorksService,
    private readonly _reportsService: ReportsService,
    private readonly _workStateFactory: WorkFormStateFactory
  ) {
  }

  public create(id: string, spinner: Spinner, destroyRef: DestroyRef): ReportFormState {
    return new ReportFormState(
      id,
      this._worksService,
      this._reportsService,
      this._workStateFactory,
      spinner,
      destroyRef
    );
  }
}
