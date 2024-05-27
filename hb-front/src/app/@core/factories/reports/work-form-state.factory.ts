import {EventTypesService} from "@core/services";
import {WorkFormState} from "@core/states";
import {IReportDetail, IWork} from "@core/models";
import {DestroyRef, Injectable} from "@angular/core";
import {Spinner} from "@core/utils";

@Injectable({providedIn: "root"})
export class WorkFormStateFactory {
  constructor(
    private readonly _eventTypesService: EventTypesService
  ) {
  }

  public create(report: IReportDetail, work: IWork, spinner: Spinner, destroyRef: DestroyRef): WorkFormState {
    return new WorkFormState(
      work,
      report,
      this._eventTypesService,
      spinner,
      destroyRef
    );
  }
}
