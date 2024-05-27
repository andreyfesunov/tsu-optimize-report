import {EventTypesService} from "@core/services";
import {IEventType, IReportDetail, IWork} from "@core/models";
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

export class WorkFormState {
  public constructor(
    public readonly work: IWork,
    private readonly _report: IReportDetail,
    private readonly _eventTypesService: EventTypesService,
    private readonly _destroyRef: DestroyRef
  ) {
  }

  private readonly _eventsMemo$: { [key: number]: Observable<IEventType[]> } = {};

  private readonly _events$: Observable<IEventType[]> = this._eventTypesService.getAllForReport(this._report.id, this.work.id).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  public readonly states$: BehaviorSubject<EventFormState[]> = new BehaviorSubject<EventFormState[]>([]);

  private readonly _eventControlsChanges$: Observable<(string | null)[]> = this.states$.pipe(
    distinctUntilChanged((prev, curr) => prev.length === curr.length),
    map((states) => new FormArray(states.map(state => state.formControl.controls.event.controls.eventType))),
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

  public addState(): void {
    const index = this.states$.value.length;
    const state = new EventFormState();

    this._availableEvents$(index).pipe(takeUntilDestroyed(this._destroyRef)).subscribe(state.events$);

    this.states$.next([...this.states$.value, state]);
  }

  public readonly addStateDisabled$: Observable<boolean> = combineLatest([this._events$, this._eventControlsChanges$]).pipe(
    map(([events, selectedEvents]) => events.length === selectedEvents.length)
  )
}
