import {DestroyRef, Injectable} from "@angular/core";
import {EventsService} from "@core/services";
import {EventFormState} from "@core/states";
import {IEvent} from "@core/models";

@Injectable({providedIn: "root"})
export class EventFormStateFactory {
  constructor(
    private readonly _eventsService: EventsService
  ) {
  }

  public create(
    reportId: string,
    event: IEvent | null = null,
    destroyRef: DestroyRef
  ): EventFormState {
    return new EventFormState(
      reportId,
      event,
      this._eventsService,
      destroyRef
    );
  }
}
