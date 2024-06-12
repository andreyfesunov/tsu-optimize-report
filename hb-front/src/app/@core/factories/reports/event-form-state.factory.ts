import {DestroyRef, Injectable} from "@angular/core";
import {EventsService} from "@core/services";
import {CommentFormState, EventFormState, LessonFormState} from "@core/states";
import {IComment, IEvent, ILesson} from "@core/models";

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
      this,
      destroyRef
    );
  }

  public createLesson(
    eventId: string,
    lesson: ILesson | null = null,
    destroyRef: DestroyRef
  ): LessonFormState {
    return new LessonFormState(
      eventId,
      lesson,
      this._eventsService,
      destroyRef
    )
  }

  public createComment(
    eventId: string,
    comment: IComment | null = null,
    destroyRef: DestroyRef
  ): CommentFormState {
    return new CommentFormState(
      eventId,
      comment,
      this._eventsService,
      destroyRef
    )
  }
}
