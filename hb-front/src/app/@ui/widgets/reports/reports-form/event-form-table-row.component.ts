import {Component, input} from "@angular/core";
import {IEventType, ITableColumn} from "@core/models";
import {EventFormState} from "@core/states";
import {AsyncPipe, NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatFormField, MatSuffix} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {MatSelect} from "@angular/material/select";

@Component({
  selector: "tr[app-event-form-table-row]",
  standalone: true,
  imports: [
    AsyncPipe,
    FormsModule,
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
    ReactiveFormsModule,
    MatSelect,
    MatAutocomplete,
    MatAutocompleteTrigger
  ],
  template: `
    <td
      *ngFor="let col of defaultCols()"
      class="tsu-table-td"
      [class.tsu-table-td--center]="col.align === 'center'"
    >
      <ng-container [ngSwitch]="col.id">
        <ng-container *ngSwitchCase="ReportItemField.ENTITY_NAME">
          <ng-container *ngIf="state().eventForm.controls.eventType as control">
            <ng-container *ngIf="state().events$ | async as eventTypes">
              <mat-form-field *ngIf="eventTypes.length !== 0" appearance="outline" style="width: 100%">
                <input [readonly]="control.value !== null" [formControl]="control" [matAutocomplete]="auto"
                       placeholder="Выберите событие" matInput>

                <mat-autocomplete #auto="matAutocomplete" [displayWith]="displayFn(eventTypes)">
                  <mat-option *ngFor="let eventType of eventTypes"
                              [value]="eventType.id">{{ eventType.name }}
                  </mat-option>
                </mat-autocomplete>
              </mat-form-field>
            </ng-container>
          </ng-container>
        </ng-container>

        <ng-container *ngSwitchCase="ReportItemField.START_DATE">
          <mat-form-field appearance="outline">
            <input [formControl]="state().eventForm.controls.startDate" [matDatepicker]="dp1"
                   matInput>
            <mat-datepicker-toggle matSuffix [for]="dp1"></mat-datepicker-toggle>
            <mat-datepicker #dp1></mat-datepicker>
          </mat-form-field>
        </ng-container>

        <ng-container *ngSwitchCase="ReportItemField.END_DATE">
          <mat-form-field appearance="outline">
            <input [formControl]="state().eventForm.controls.endDate" [matDatepicker]="dp2"
                   matInput>
            <mat-datepicker-toggle matSuffix [for]="dp2"></mat-datepicker-toggle>
            <mat-datepicker #dp2></mat-datepicker>
          </mat-form-field>
        </ng-container>
      </ng-container>
    </td>
  `
})
export class EventFormTableRowComponent {
  public readonly state = input.required<EventFormState>();
  public readonly defaultCols = input.required<ITableColumn<ReportItemField>[]>();

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
