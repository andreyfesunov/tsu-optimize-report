import { SemesterEnum } from '@core/models';

export interface IEventCreateDto {
  readonly stateUserId: string;
  readonly eventTypeId: string;
  readonly startedAt: Date;
  readonly endedAt: Date;
  readonly semestrId: SemesterEnum;
}

export interface IEventUpdateDto {
  readonly id: string;
  readonly startedAt: Date;
  readonly endedAt: Date;
}

export interface ILessonCreateDto {
  readonly eventId: string;
  readonly lessonTypeId: string;
  readonly planDate: number | null;
  readonly factDate: number | null;
}

export interface ILessonUpdateDto {
  readonly id: string;
  readonly planDate: number | null;
  readonly factDate: number | null;
}

export interface ICommentCreateDto {
  readonly eventId: string;
  readonly content: string;
  readonly planDate: number | null;
  readonly factDate: number | null;
}

export interface ICommentUpdateDto extends Omit<ICommentCreateDto, 'eventId'> {
  readonly id: string;
}
