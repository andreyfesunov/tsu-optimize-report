import {Component, input} from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import {BehaviorSubject, map, Observable, shareReplay, switchMap} from "rxjs";
import {IEventType, ITableColumn, IWork} from "@core/models";
import {EventTypesService, WorksService} from "@core/abstracts";
import {AsyncPipe, NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";
import {ScrollableComponent, SpinnerComponent, TableComponent} from "@ui/widgets";
import {Spinner, withSpinner} from "@core/utils";
import {FormArray, FormControl, FormGroup, ReactiveFormsModule} from "@angular/forms";
import {SubscriptionController} from "@core/controllers";
import {MatButton, MatIconButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {MatFormField, MatLabel, MatSuffix} from "@angular/material/form-field";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatInput} from "@angular/material/input";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";

@Component({
  selector: 'app-reports-tabs',
  standalone: true,
  imports: [
    MatTabsModule,
    NgIf,
    AsyncPipe,
    NgForOf,
    SpinnerComponent,
    TableComponent,
    MatIconButton,
    MatIcon,
    MatLabel,
    MatButton,
    NgSwitchCase,
    MatAutocomplete,
    MatAutocompleteTrigger,
    MatFormField,
    MatInput,
    MatOption,
    ReactiveFormsModule,
    NgSwitch,
    MatDatepicker,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatSuffix,
    ScrollableComponent
  ],
  templateUrl: 'reports-tabs.component.html',
  styleUrl: 'reports-tabs.component.scss'
})
export class ReportsTabsComponent extends SubscriptionController {
  constructor(
    private readonly _worksService: WorksService,
    private readonly _eventTypesService: EventTypesService
  ) {
    super();
  }

  public readonly id = input.required<string>();

  protected readonly defaultCols = defaultCols;

  protected readonly workForm: IWorkForm = new FormArray<IEventFormGroup>([]);

  protected readonly spinner = new Spinner();

  protected readonly works$: Observable<IWork[]> = withSpinner(this._worksService.getAll(), this.spinner).pipe(
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  protected readonly tabChanged$: BehaviorSubject<number> = new BehaviorSubject<number>(0);

  protected readonly currentWork$: Observable<IWork> = this.tabChanged$.pipe(
    switchMap((index) => this.works$.pipe(map((works) => works[index]))),
    shareReplay({bufferSize: 1, refCount: true})
  );

  protected readonly events$: Observable<IEventType[]> = this.currentWork$.pipe(
    switchMap((work) => this._eventTypesService.getAllForReport(this.id(), work.id)),
    shareReplay({bufferSize: 1, refCount: true})
  );

  protected readonly ReportItemField = ReportItemField;

  protected addEventForm(): void {
    const eventForm: FormGroup<IEventForm> = new FormGroup<IEventForm>({
      eventType: new FormControl<string | null>(null),
      startDate: new FormControl<Date>(new Date(), {nonNullable: true}),
      endDate: new FormControl<Date>(new Date(), {nonNullable: true})
    });

    const formGroup: IEventFormGroup = new FormGroup({
      event: eventForm,
      lessons: new FormArray<FormControl<ILessonForm>>([])
    });

    this.workForm.push(formGroup);
  }

  protected displayFn(opts: IEventType[]) {
    return (id: string) => {
      const eventType = opts.find(v => v.id === id);
      return eventType ? eventType.name : '';
    }
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
  lessons: FormArray<FormControl<ILessonForm>>
}>;

export type IWorkForm = FormArray<IEventFormGroup>;

export type IReportForm = FormArray<FormControl<IWorkForm>>;

export enum ReportItemField {
  ENTITY_NAME = 'ENTITY_NAME',
  START_DATE = 'START_DATE',
  END_DATE = 'END_DATE',
  PLAN = 'PLAN',
  FACT = 'FACT'
}

const defaultCols: ITableColumn[] = [
  {
    id: ReportItemField.ENTITY_NAME,
    order: 1,
    text: 'Наименование работы'
  },
  {
    id: ReportItemField.START_DATE,
    order: 2,
    text: 'Начало работы',
  },
  {
    id: ReportItemField.END_DATE,
    order: 3,
    text: 'Окончание работы'
  },
  {
    id: ReportItemField.PLAN,
    order: 4,
    text: 'План'
  },
  {
    id: ReportItemField.FACT,
    order: 5,
    text: 'Факт'
  }
]
