import {Component, computed, DestroyRef, input} from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import {BehaviorSubject} from "rxjs";
import {IEventType, ITableColumn} from "@core/models";
import {AsyncPipe, NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";
import {ScrollableComponent, SpinnerComponent, TableComponent} from "@ui/widgets";
import {Spinner} from "@core/utils";
import {ReactiveFormsModule} from "@angular/forms";
import {SubscriptionController} from "@core/controllers";
import {MatButton, MatIconButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {MatFormField, MatLabel, MatSuffix} from "@angular/material/form-field";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatInput} from "@angular/material/input";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {ReportsFormStateFactory} from "@core/factories";

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
    private readonly _reportStateFactory: ReportsFormStateFactory,
    private readonly _destroyRef: DestroyRef
  ) {
    super();
  }

  public readonly id = input.required<string>();

  protected readonly state = computed(() => this._reportStateFactory.create(this.id(), this._destroyRef));

  protected readonly defaultCols = defaultCols;

  protected readonly spinner = new Spinner();

  protected readonly tabChanged$: BehaviorSubject<number> = new BehaviorSubject<number>(0);

  protected readonly ReportItemField = ReportItemField;

  protected displayFn(opts: IEventType[]) {
    return (id: string) => {
      const eventType = opts.find(v => v.id === id);
      return eventType ? eventType.name : '';
    }
  }
}

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
