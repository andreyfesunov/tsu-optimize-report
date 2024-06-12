import {EventsService} from "@core/services";
import {DestroyRef} from "@angular/core";
import {ILesson, ILessonType} from "@core/models";
import {FormControl, FormGroup} from "@angular/forms";
import {BehaviorSubject, map, merge, Observable, switchMap} from "rxjs";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import {exists, required} from "@core/utils";
import {ILessonCreateDto, ILessonUpdateDto} from "@core/dtos";

export class LessonFormState {
  constructor(
    private readonly _eventId: string,
    private readonly _lesson: ILesson | null,
    private readonly _eventsService: EventsService,
    private readonly _destroyRef: DestroyRef
  ) {
    this._init();
  }

  private readonly _id$: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

  public readonly types$: BehaviorSubject<ILessonType[]> = new BehaviorSubject<ILessonType[]>([]);

  public readonly form: FormGroup<ILessonForm> = new FormGroup<ILessonForm>({
    lessonType: new FormControl<string | null>(null),
    fact: new FormControl<number | null>(null, {nonNullable: true}),
    plan: new FormControl<number | null>(null, {nonNullable: true}),
  })

  private _init(): void {
    this._lesson && this._bindForm(this._lesson);
    this._lesson && this._id$.next(this._lesson.id);

    merge(
      this.form.valueChanges,
      this.form.controls.lessonType.valueChanges
    ).pipe(
      takeUntilDestroyed(this._destroyRef),
      map(() => this._id$.value ? this._update() : this._create()),
      exists(),
      switchMap((observable) => observable)
    ).subscribe((lesson) => {
      if (this._id$.value !== lesson.id) {
        this._id$.next(lesson.id);
      }
    })
  }

  private _bindForm(lesson: ILesson): void {
    this.form.patchValue({
      lessonType: lesson.lessonType.id,
      fact: lesson.factDate,
      plan: lesson.planDate
    }, {emitEvent: false})
  }

  private _create(): Observable<ILesson> | null {
    const form = this.form.getRawValue();

    // TODO fix bug with valueChanges on changing autocomplete
    if (form.lessonType === null) {
      return null;
    }

    const request: ILessonCreateDto = {
      eventId: this._eventId,
      lessonTypeId: required(form.lessonType),
      planDate: form.plan,
      factDate: form.fact,
    };

    return this._eventsService.createLesson(request);
  }

  private _update(): Observable<ILesson> | null {
    const form = this.form.getRawValue();

    // TODO fix bug with valueChanges on changing autocomplete
    if (form.lessonType === null) {
      return null;
    }

    const request: ILessonUpdateDto = {
      id: required(this._id$.value),
      planDate: form.plan,
      factDate: form.fact
    };

    return this._eventsService.updateLesson(request);
  }
}

export interface ILessonForm {
  lessonType: FormControl<string | null>;
  plan: FormControl<number | null>;
  fact: FormControl<number | null>;
}
