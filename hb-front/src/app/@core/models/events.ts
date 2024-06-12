import {IEventType} from "@core/models/event-type";
import {ILesson} from "@core/models/lesson";
import {IComment} from "@core/models/comment";

export interface IEvent {
  readonly id: string;
  readonly eventType: IEventType;
  readonly startedAt: Date;
  readonly endedAt: Date;

  readonly lessons: readonly ILesson[];
  readonly comments: readonly IComment[];
}
