import {ILessonType} from "@core/models/lesson-type";

export interface ILesson {
  readonly id: string;
  readonly lessonType: ILessonType;
  readonly factDate: number | null;
  readonly planDate: number | null;
}
