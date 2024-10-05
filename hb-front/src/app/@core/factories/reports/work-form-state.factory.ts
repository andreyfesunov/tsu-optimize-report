import {EventsService, EventTypesService} from "@core/services";
import {WorkFormState} from "@core/states";
import {IReportDetail, IWork} from "@core/models";
import {DestroyRef, inject, Injectable} from "@angular/core";
import {Spinner} from "@core/utils";
import {EventFormStateFactory} from "@core/factories";

@Injectable({providedIn: "root"})
export class WorkFormStateFactory {
  private readonly _eventTypesService = inject(EventTypesService)
  private readonly _eventStateFactory = inject(EventFormStateFactory);
  private readonly _eventsService = inject(EventsService);

  public create(index: number, report: IReportDetail, work: IWork, spinner: Spinner, destroyRef: DestroyRef): WorkFormState {
    return new WorkFormState(
      index,
      work,
      report,
      this._eventTypesService,
      this._eventStateFactory,
      this._eventsService,
      spinner,
      destroyRef
    );
  }
}
