import {BehaviorSubject} from "rxjs";
import {FormArray, FormControl, FormGroup, Validators} from "@angular/forms";
import {IEventType} from "@core/models";

export class EventFormState {
  public readonly events$: BehaviorSubject<IEventType[]> = new BehaviorSubject<IEventType[]>([]);
  public readonly formControl: IEventFormGroup = new FormGroup({
    event: new FormGroup<IEventForm>({
      eventType: new FormControl<string | null>(null, [Validators.required]),
      startDate: new FormControl<Date>(new Date(), {nonNullable: true}),
      endDate: new FormControl<Date>(new Date(), {nonNullable: true})
    }),
    lessons: new FormArray<FormGroup<ILessonForm>>([])
  });
}

export interface IEventForm {
  eventType: FormControl<string | null>;
  startDate: FormControl<Date>;
  endDate: FormControl<Date>;
}

export interface ILessonForm {
  lessonType: FormControl<string | null>;
  plan: FormControl<number | null>;
  fact: FormControl<number | null>;
}

export type IEventFormGroup = FormGroup<{
  event: FormGroup<IEventForm>;
  lessons: FormArray<FormGroup<ILessonForm>>
}>;
