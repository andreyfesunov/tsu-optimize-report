import {BehaviorSubject, map, merge, Observable, switchMap, tap} from "rxjs";
import {FormArray, FormControl, FormGroup, Validators} from "@angular/forms";
import {IEvent, IEventType} from "@core/models";
import {EventsService} from "@core/services";
import {DestroyRef} from "@angular/core";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import {IEventCreateDto, IEventUpdateDto} from "@core/dtos";
import {exists, required} from "@core/utils";

export class EventFormState {
  constructor(
    private readonly _reportId: string,
    private readonly _event: IEvent | null,
    private readonly _eventsService: EventsService,
    private readonly _destroyRef: DestroyRef
  ) {
    this._init();
  }

  private readonly _id$: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

  public readonly events$: BehaviorSubject<IEventType[]> = new BehaviorSubject<IEventType[]>([]);

  public readonly eventForm: FormGroup<IEventForm> = new FormGroup<IEventForm>({
    eventType: new FormControl<string | null>(null, [Validators.required]),
    startDate: new FormControl<Date>(new Date(), {nonNullable: true}),
    endDate: new FormControl<Date>(new Date(), {nonNullable: true})
  });

  private _init(): void {
    this._event && this._bindForm(this._event);
    this._event && this._id$.next(this._event.id);
    
    merge(
      this.eventForm.valueChanges,
      this.eventForm.controls.eventType.valueChanges
    ).pipe(
      takeUntilDestroyed(this._destroyRef),
      tap((v) => console.log(v)),
      map(() => this._id$.value ? this._update() : this._create()),
      exists(),
      switchMap((observable) => observable)
    ).subscribe((event) => {
      if (this._id$.value !== event.id) {
        this._id$.next(event.id);
      }
    })
  }

  private _disableAutocomplete(ctl: FormControl<string | null>): void {
    const disable = ctl.value !== null;

    if (disable) {
      ctl.disable({emitEvent: false, onlySelf: true});
    } else {
      ctl.enable({emitEvent: false, onlySelf: true});
    }
  }

  private _bindForm(event: IEvent): void {
    const form = this.eventForm;

    console.log(event);

    form.patchValue({
      eventType: event.eventType.id,
      startDate: event.startedAt,
      endDate: event.endedAt
    }, {emitEvent: false})
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

    console.log(this.eventForm.value, form);

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

export interface ILessonForm {
  lessonType: FormControl<string | null>;
  plan: FormControl<number | null>;
  fact: FormControl<number | null>;
}

export type IEventFormGroup = FormGroup<{
  event: FormGroup<IEventForm>;
  lessons: FormArray<FormGroup<ILessonForm>>
}>;
