import {EventTypesService} from "@core/services";
import {IEvent, IEventType, IReportDetail, IWork} from "@core/models";
import {
  BehaviorSubject,
  combineLatest,
  concat,
  distinctUntilChanged,
  map,
  Observable,
  of,
  shareReplay,
  switchMap
} from "rxjs";
import {EventFormState} from "@core/states";
import {FormArray} from "@angular/forms";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import {DestroyRef} from "@angular/core";
import {Spinner, withSpinner} from "@core/utils";
import {EventFormStateFactory} from "@core/factories";

export class WorkFormState {
  public constructor(
    public readonly index: number,
    public readonly work: IWork,
    private readonly _report: IReportDetail,
    private readonly _eventTypesService: EventTypesService,
    private readonly _eventStateFactory: EventFormStateFactory,
    private readonly _spinner: Spinner,
    private readonly _destroyRef: DestroyRef
  ) {
    this._report.events
      .filter(x => x.eventType.work.id === this.work.id)
      .forEach((event) => this.addEvent(event));
  }

  private readonly _eventsMemo$: { [key: number]: Observable<IEventType[]> } = {};

  private readonly _events$: Observable<IEventType[]> = withSpinner(this._eventTypesService.getAllForReport(
    this._report.id,
    this.work.id,
    this.index === 0
  ), this._spinner).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  public readonly states$: BehaviorSubject<EventFormState[]> = new BehaviorSubject<EventFormState[]>([]);

  private readonly _eventControlsChanges$: Observable<(string | null)[]> = this.states$.pipe(
    distinctUntilChanged((prev, curr) => prev.length === curr.length),
    map((states) => new FormArray(states.map((state) => state.eventForm.controls.eventType))),
    switchMap((control) => concat(of(control.value), control.valueChanges)),
    distinctUntilChanged((prev, curr) => JSON.stringify(prev) === JSON.stringify(curr)),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  private readonly _availableEvents$ = (index: number) => (index in this._eventsMemo$) ? this._eventsMemo$[index] : (this._eventsMemo$[index] = this._eventControlsChanges$.pipe(
    switchMap((eventChanges) => this._events$.pipe(map((events) => {
      const selectedEvents = eventChanges.filter((v): v is string => !!v);

      return events.filter((event) => !selectedEvents.includes(event.id) || eventChanges[index] === event.id);
    }))),
    shareReplay({refCount: true, bufferSize: 1})
  ));

  public addEvent(event: IEvent | null = null): void {
    const index = this.states$.value.length;
    const state = this._eventStateFactory.create(
      this._report.id,
      event,
      this._destroyRef
    );

    this._availableEvents$(index).pipe(takeUntilDestroyed(this._destroyRef)).subscribe(state.events$);

    this.states$.next([...this.states$.value, state]);
  }

  public readonly addStateDisabled$: Observable<boolean> = combineLatest([this._events$, this._eventControlsChanges$]).pipe(
    map(([events, selectedEvents]) => events.length === selectedEvents.length)
  )
}
