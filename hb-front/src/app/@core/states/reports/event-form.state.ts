import {
  BehaviorSubject,
  combineLatest,
  concat,
  distinctUntilChanged,
  map,
  merge,
  Observable,
  of,
  shareReplay,
  switchMap
} from "rxjs";
import {FormArray, FormControl, FormGroup, Validators} from "@angular/forms";
import {IComment, IEvent, IEventType, ILesson, ILessonType} from "@core/models";
import {EventsService} from "@core/services";
import {DestroyRef} from "@angular/core";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import {IEventCreateDto, IEventUpdateDto} from "@core/dtos";
import {exists, required} from "@core/utils";
import {CommentFormState, LessonFormState} from "@core/states";
import {EventFormStateFactory} from "@core/factories";

export class EventFormState {
  constructor(
    private readonly _reportId: string,
    public readonly event: IEvent | null,
    private readonly _eventsService: EventsService,
    private readonly _eventStateFactory: EventFormStateFactory,
    private readonly _destroyRef: DestroyRef
  ) {
    this._init();
  }

  /** Lesson form state */

  private readonly _typesMemo$: { [key: number]: Observable<ILessonType[]> } = {};

  public readonly lessonFormStates$: BehaviorSubject<LessonFormState[]> = new BehaviorSubject<LessonFormState[]>([]);

  private readonly _lessonControlsChanges$: Observable<(string | null)[]> = this.lessonFormStates$.pipe(
    distinctUntilChanged((prev, curr) => prev.length === curr.length),
    map((states) => new FormArray(states.map((state) => state.form.controls.lessonType))),
    switchMap((control) => concat(of(control.value), control.valueChanges)),
    distinctUntilChanged((prev, curr) => JSON.stringify(prev) === JSON.stringify(curr)),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  private readonly _types$ = this._eventsService.getLessonTypes(this._reportId);

  public readonly canAddLesson$: Observable<boolean> = combineLatest([this.lessonFormStates$, this._types$]).pipe(map(([states, types]) => states.length < types.length));

  private readonly _availableTypes$ = (index: number) => (index in this._typesMemo$) ? this._typesMemo$[index] : (this._typesMemo$[index] = this._lessonControlsChanges$.pipe(
    switchMap((typeChanges) => this._types$.pipe(map((types) => {
      const selectedTypes = typeChanges.filter((v): v is string => !!v);

      return types.filter((type) => !selectedTypes.includes(type.id) || typeChanges[index] === type.id);
    }))),
    shareReplay({refCount: true, bufferSize: 1})
  ));

  /** Comment form state */

  public readonly commentFormStates$: BehaviorSubject<CommentFormState[]> = new BehaviorSubject<CommentFormState[]>([]);

  /** Event data */

  public readonly deleteDisabled$ = combineLatest([this.commentFormStates$, this.lessonFormStates$]).pipe(
    map(([comments, lessons]) => (comments.length + lessons.length) > 0)
  )

  private readonly _id$: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

  public readonly editable$: Observable<boolean> = this._id$.pipe(map((v) => !!v));

  public readonly events$: BehaviorSubject<IEventType[]> = new BehaviorSubject<IEventType[]>([]);

  public readonly eventForm: FormGroup<IEventForm> = new FormGroup<IEventForm>({
    eventType: new FormControl<string | null>(null, [Validators.required]),
    startDate: new FormControl<Date>(new Date(), {nonNullable: true}),
    endDate: new FormControl<Date>(new Date(), {nonNullable: true})
  });

  private _init(): void {
    this.event && this._id$.next(this.event.id);
    this.event && this._bindForm(this.event);

    merge(
      this.eventForm.valueChanges,
      this.eventForm.controls.eventType.valueChanges
    ).pipe(
      takeUntilDestroyed(this._destroyRef),
      map(() => this._id$.value ? this._update() : this._create()),
      exists(),
      switchMap((observable) => observable)
    ).subscribe((event) => {
      if (this._id$.value !== event.id) {
        this._id$.next(event.id);
      }
    })
  }

  private _bindForm(event: IEvent): void {
    const form = this.eventForm;

    this._bindLessons(event.lessons);
    this._bindComments(event.comments);

    form.patchValue({
      eventType: event.eventType.id,
      startDate: event.startedAt,
      endDate: event.endedAt
    }, {emitEvent: false})
  }

  private _bindLessons(lessons: readonly ILesson[]): void {
    lessons.forEach((lesson) => this.addLesson(lesson));
  }

  public addLesson(lesson: ILesson | null = null): void {
    const eventId = required(this._id$.value);
    const index = this.lessonFormStates$.value.length;
    const state = this._eventStateFactory.createLesson(eventId, lesson, this._destroyRef);

    this._availableTypes$(index).pipe(takeUntilDestroyed(this._destroyRef)).subscribe(state.types$);

    this.lessonFormStates$.next([
      ...this.lessonFormStates$.value,
      state
    ]);
  }

  private _bindComments(comments: readonly IComment[]): void {
    comments.forEach((comment) => this.addComment(comment));
  }

  public addComment(comment: IComment | null = null): void {
    const eventId = required(this._id$.value);
    const state = this._eventStateFactory.createComment(eventId, comment, this._destroyRef);

    this.commentFormStates$.next([
      ...this.commentFormStates$.value,
      state
    ]);
  }

  public deleteComment(index: number): void {
    const comments = this.commentFormStates$.value;
    const deleted = comments[index].comment;

    if (deleted) {
      this._eventsService.deleteComment(deleted.id).pipe(
        takeUntilDestroyed(this._destroyRef)
      ).subscribe(() => this.commentFormStates$.next(this.commentFormStates$.value.filter((_, arrIndex) => arrIndex !== index)))
    } else {
      this.commentFormStates$.next(this.commentFormStates$.value.filter((_, arrIndex) => arrIndex !== index));
    }
  }

  public deleteLesson(index: number): void {
    const lessons = this.lessonFormStates$.value;
    const deleted = lessons[index].lesson;

    if (deleted) {
      this._eventsService.deleteLesson(deleted.id).pipe(
        takeUntilDestroyed(this._destroyRef)
      ).subscribe(() => this.lessonFormStates$.next(this.lessonFormStates$.value.filter((_, arrIndex) => arrIndex !== index)));
    } else {
      this.lessonFormStates$.next(this.lessonFormStates$.value.filter((_, arrIndex) => arrIndex !== index));
    }
  }

  private _create(): Observable<IEvent> | null {
    const form = this.eventForm.getRawValue();

    // TODO fix bug with valueChanges on changing autocomplete
    if (form.eventType === null) {
      return null;
    }

    const request: IEventCreateDto = {
      eventTypeId: required(form.eventType),
      stateUserId: this._reportId,
      endedAt: form.endDate,
      startedAt: form.startDate
    };

    return this._eventsService.create(request);
  }

  private _update(): Observable<IEvent> | null {
    const form = this.eventForm.getRawValue();

    // TODO fix bug with valueChanges on changing autocomplete
    if (form.eventType === null) {
      return null;
    }

    const request: IEventUpdateDto = {
      id: required(this._id$.value),
      endedAt: form.endDate,
      startedAt: form.startDate
    };

    return this._eventsService.update(request);
  }
}

export interface IEventForm {
  eventType: FormControl<string | null>;
  startDate: FormControl<Date>;
  endDate: FormControl<Date>;
}
