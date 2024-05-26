import {IEventType} from "@core/models/event-type";

export interface IEvent {
  readonly id: string;
  readonly eventType: IEventType;
  readonly startedAt: Date;
  readonly endedAt: Date;
}
