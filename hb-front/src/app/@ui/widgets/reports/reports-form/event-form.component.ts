import {Component, input} from "@angular/core";
import {EventFormState} from "@core/states";
import {AsyncPipe, NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatButton} from "@angular/material/button";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatFormField, MatSuffix} from "@angular/material/form-field";
import {MatIcon} from "@angular/material/icon";
import {MatInput} from "@angular/material/input";
import {EventFormTableRowComponent, LessonFormTableRowComponent, ReportItemField, TableComponent} from "@ui/widgets";
import {ITableColumn} from "@core/models";

@Component({
  selector: "app-report-event-form",
  standalone: true,
  imports: [
    AsyncPipe,
    FormsModule,
    MatAutocomplete,
    MatAutocompleteTrigger,
    MatButton,
    MatDatepicker,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatFormField,
    MatIcon,
    MatInput,
    MatOption,
    MatSuffix,
    NgForOf,
    NgIf,
    NgSwitchCase,
    TableComponent,
    ReactiveFormsModule,
    NgSwitch,
    EventFormTableRowComponent,
    LessonFormTableRowComponent
  ],
  template: `
    <table app-table [cols]="defaultCols" [itemsCount]="1" [bordered]="true">
      <tr
        app-event-form-table-row
        [defaultCols]="defaultCols"
        [state]="state()"
      ></tr>
      <tr
        *ngFor="let state of state().lessonFormStates$ | async"
        app-lesson-form-table-row
        [defaultCols]="defaultCols"
        [state]="state"
      ></tr>
      <tr *ngIf="state().editable$ | async">
        <td
          *ngIf="index() === 0"
          class="tsu-table-td"
        >
          <button mat-button (click)="state().addLesson()">
            <mat-icon>add_circle</mat-icon>
            Добавить учебную дисциплину
          </button>
        </td>
      </tr>
    </table>
  `
})
export class EventFormComponent {
  public readonly state = input.required<EventFormState>();
  public readonly index = input.required<number>();

  protected readonly defaultCols = defaultCols;
}

const defaultCols: ITableColumn<ReportItemField>[] = [
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
