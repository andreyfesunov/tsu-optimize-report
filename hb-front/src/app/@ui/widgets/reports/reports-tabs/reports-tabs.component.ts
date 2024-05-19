import {Component, input} from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import {Observable, shareReplay} from "rxjs";
import {ITableColumn, IWork} from "@core/models";
import {WorksService} from "@core/abstracts";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {SpinnerComponent, TableComponent} from "@ui/widgets";
import {Spinner, withSpinner} from "@core/utils";
import {FormArray, FormControl, FormGroup} from "@angular/forms";
import {SubscriptionController} from "@core/controllers";
import {MatIconButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";

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
    MatIcon
  ],
  templateUrl: 'reports-tabs.component.html',
  styleUrl: 'reports-tabs.component.scss'
})
export class ReportsTabsComponent extends SubscriptionController {
  constructor(
    private readonly _worksService: WorksService
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
  event: FormControl<IEventForm>;
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
