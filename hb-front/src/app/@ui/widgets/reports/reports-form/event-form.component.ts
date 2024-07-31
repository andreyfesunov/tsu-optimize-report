import {Component, input, output} from "@angular/core";
import {EventFormState} from "@core/states";
import {AsyncPipe, NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatButton, MatIconButton} from "@angular/material/button";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatFormField, MatSuffix} from "@angular/material/form-field";
import {MatIcon} from "@angular/material/icon";
import {MatInput} from "@angular/material/input";
import {
  CommentFormTableRowComponent,
  EventFormTableRowComponent,
  LessonFormTableRowComponent,
  ReportItemField,
  TableComponent
} from "@ui/widgets";
import {ITableColumn} from "@core/models";
import {LayoutRefDirective, LayoutRefs} from "@core/directives";
import {MatTooltip} from "@angular/material/tooltip";

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
    LessonFormTableRowComponent,
    CommentFormTableRowComponent,
    LayoutRefDirective,
    MatIconButton,
    MatTooltip
  ],
  template: `
    <table style="padding-bottom: 12px" app-table [cols]="defaultCols" [itemsCount]="1" [bordered]="true">
      <tr
        app-event-form-table-row
        [cols]="defaultCols"
        [item]="state()"
        [clickable]="false"
      >
        <ng-template [appLayoutRef]="LayoutRefs.ACTIONS">
          <button matTooltip="Добавить учебную дисциплину" *ngIf="workIndex() === 0"
                  [disabled]="(state().editable$ | async) === false || (state().canAddLesson$ | async) === false"
                  mat-icon-button
                  (click)="state().addLesson()">
            <mat-icon>add_circle</mat-icon>
          </button>
          <button matTooltip="Добавить запись" *ngIf="workIndex() !== 0"
                  [disabled]="(state().editable$ | async) === false"
                  mat-icon-button (click)="state().addComment()">
            <mat-icon>add_circle</mat-icon>
          </button>
          <button mat-icon-button [disabled]="state().deleteDisabled$ | async" (click)="deleteState.emit()">
            <mat-icon>delete</mat-icon>
          </button>
        </ng-template>
      </tr>

      <tr
        *ngFor="let lessonState of state().lessonFormStates$ | async; index as index"
        app-lesson-form-table-row
        [cols]="defaultCols"
        [item]="lessonState"
        [clickable]="false"
      >
        <ng-template [appLayoutRef]="LayoutRefs.ACTIONS">
          <button mat-icon-button (click)="state().deleteLesson(index)">
            <mat-icon>delete</mat-icon>
          </button>
        </ng-template>
      </tr>
      
      <tr
        *ngFor="let commentState of state().commentFormStates$ | async; index as index"
        app-comment-form-table-row
        [cols]="defaultCols"
        [item]="commentState"
        [clickable]="false"
      >
        <ng-template [appLayoutRef]="LayoutRefs.ACTIONS">
          <button mat-icon-button (click)="state().deleteComment(index)">
            <mat-icon>delete</mat-icon>
          </button>
        </ng-template>
      </tr>
    </table>
  `
})
export class EventFormComponent {
  public readonly state = input.required<EventFormState>();
  public readonly workIndex = input.required<number>();

  public readonly deleteState = output();

  protected readonly defaultCols = defaultCols;
  protected readonly LayoutRefs = LayoutRefs;
}

const defaultCols: ITableColumn<ReportItemField>[] = [
  {
    id: ReportItemField.ACTIONS,
    order: 1,
  },
  {
    id: ReportItemField.ENTITY_NAME,
    order: 2,
    text: 'Наименование работы'
  },
  {
    id: ReportItemField.START_DATE,
    order: 3,
    text: 'Начало работы',
  },
  {
    id: ReportItemField.END_DATE,
    order: 4,
    text: 'Окончание работы'
  },
  {
    id: ReportItemField.PLAN,
    order: 5,
    text: 'План'
  },
  {
    id: ReportItemField.FACT,
    order: 6,
    text: 'Факт'
  }
]
