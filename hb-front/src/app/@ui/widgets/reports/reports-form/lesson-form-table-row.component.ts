import {ILessonType, ITableColumn} from "@core/models";
import {ReportItemField} from "@ui/widgets";
import {Component, input} from "@angular/core";
import {LessonFormState} from "@core/states";
import {AsyncPipe, NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatFormField, MatSuffix} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";

@Component({
  selector: "tr[app-lesson-form-table-row]",
  standalone: true,
  imports: [
    AsyncPipe,
    FormsModule,
    MatAutocomplete,
    MatAutocompleteTrigger,
    MatDatepicker,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatFormField,
    MatInput,
    MatOption,
    MatSuffix,
    NgForOf,
    NgIf,
    NgSwitchCase,
    NgSwitch,
    ReactiveFormsModule
  ],
  template:
    `
      <td
        *ngFor="let col of defaultCols()"
        class="tsu-table-td"
        [class.tsu-table-td--center]="col.align === 'center'"
      >
        <ng-container [ngSwitch]="col.id">
          <ng-container *ngSwitchCase="ReportItemField.ENTITY_NAME">
            <ng-container *ngIf="state().form.controls.lessonType as control">
              <ng-container *ngIf="state().types$ | async as types">
                <mat-form-field *ngIf="types.length !== 0" appearance="outline" style="width: 100%">
                  <input [readonly]="control.value !== null" [formControl]="control" [matAutocomplete]="auto"
                         placeholder="Выберите дисциплину" matInput>

                  <mat-autocomplete #auto="matAutocomplete" [displayWith]="displayFn(types)">
                    <mat-option *ngFor="let type of types"
                                [value]="type.id">{{ type.name }}
                    </mat-option>
                  </mat-autocomplete>
                </mat-form-field>
              </ng-container>
            </ng-container>
          </ng-container>

          <ng-container *ngSwitchCase="ReportItemField.PLAN">
            <mat-form-field appearance="outline">
              <input [formControl]="state().form.controls.plan"
                     [min]="0"
                     type="number"
                     matInput>
            </mat-form-field>
          </ng-container>

          <ng-container *ngSwitchCase="ReportItemField.FACT">
            <mat-form-field appearance="outline">
              <input [formControl]="state().form.controls.fact"
                     [min]="0"
                     type="number"
                     matInput>
            </mat-form-field>
          </ng-container>
        </ng-container>
      </td>
    `
})
export class LessonFormTableRowComponent {
  public readonly state = input.required<LessonFormState>();
  public readonly defaultCols = input.required<ITableColumn<ReportItemField>[]>();

  protected readonly ReportItemField = ReportItemField;

  protected displayFn(opts: ILessonType[]) {
    return (id: string) => {
      const lessonType = opts.find(v => v.id === id);
      return lessonType ? lessonType.name : '';
    }
  }
}
