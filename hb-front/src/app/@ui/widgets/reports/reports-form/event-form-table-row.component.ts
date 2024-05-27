import {Component, input} from "@angular/core";
import {IEventType, ITableColumn} from "@core/models";
import {EventFormState} from "@core/states";
import {AsyncPipe, NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatFormField, MatSuffix} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";

@Component({
  selector: "tr[app-event-form-table-row]",
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
  template: `
    <td
      *ngFor="let col of defaultCols()"
      class="tsu-table-td"
      [class.tsu-table-td--center]="col.align === 'center'"
    >
      <ng-container [ngSwitch]="col.id">
        <ng-container *ngSwitchCase="ReportItemField.ENTITY_NAME">
          <mat-form-field *ngIf="state().events$ | async as eventTypes" appearance="outline">
            <input [formControl]="state().formControl.controls.event.controls.eventType" [matAutocomplete]="auto"
                   matInput>

            <mat-autocomplete #auto="matAutocomplete" [displayWith]="displayFn(eventTypes)">
              <mat-option *ngFor="let eventType of eventTypes"
                          [value]="eventType.id">{{ displayFn(eventTypes)(eventType.id) }}
              </mat-option>
            </mat-autocomplete>
          </mat-form-field>
        </ng-container>

        <ng-container *ngSwitchCase="ReportItemField.START_DATE">
          <mat-form-field appearance="outline">
            <input [formControl]="state().formControl.controls.event.controls.startDate" [matDatepicker]="dp1"
                   matInput>
            <mat-datepicker-toggle matSuffix [for]="dp1"></mat-datepicker-toggle>
            <mat-datepicker #dp1></mat-datepicker>
          </mat-form-field>
        </ng-container>

        <ng-container *ngSwitchCase="ReportItemField.END_DATE">
          <mat-form-field appearance="outline">
            <input [formControl]="state().formControl.controls.event.controls.startDate" [matDatepicker]="dp2"
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
