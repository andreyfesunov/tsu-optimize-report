import {IActivity} from "@core/models/activities";
import {ILessonType} from "@core/models/lesson-type";

export interface IRecord {
  readonly id: string;
  readonly hours: number;
  readonly activity: IActivity,
  readonly lessonType: ILessonType
}
