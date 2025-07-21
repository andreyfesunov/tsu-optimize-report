import { DestroyRef, Injectable, inject } from '@angular/core';
import { EventsService } from '@core/services';
import {
  CommentFormState,
  EventFormState,
  LessonFormState,
} from '@core/states';
import { IComment, IEvent, ILesson, SemesterEnum } from '@core/models';

@Injectable({ providedIn: 'root' })
export class EventFormStateFactory {
  private readonly _eventsService = inject(EventsService);

  public create(
    reportId: string,
    semesterId: SemesterEnum,
    event: IEvent | null = null,
    destroyRef: DestroyRef,
  ): EventFormState {
    return new EventFormState(
      reportId,
      semesterId,
      event,
      this._eventsService,
      this,
      destroyRef,
    );
  }

  public createLesson(
    eventId: string,
    lesson: ILesson | null = null,
    destroyRef: DestroyRef,
  ): LessonFormState {
    return new LessonFormState(
      eventId,
      lesson,
      this._eventsService,
      destroyRef,
    );
  }

  public createComment(
    eventId: string,
    comment: IComment | null = null,
    destroyRef: DestroyRef,
  ): CommentFormState {
    return new CommentFormState(
      eventId,
      comment,
      this._eventsService,
      destroyRef,
    );
  }
}
