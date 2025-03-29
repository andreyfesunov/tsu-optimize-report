import {Component} from "@angular/core";
import {IEventType} from "@core/models";
import {EventFormState} from "@core/states";
import {AsyncPipe, CommonModule, NgForOf, NgIf, NgSwitch, NgSwitchCase, NgTemplateOutlet} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatFormField, MatSuffix} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {TableRowController} from "@core/controllers";
import {MatTooltip} from "@angular/material/tooltip";

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
    MatAutocomplete,
    MatAutocompleteTrigger,
    NgTemplateOutlet,
    CommonModule,
    MatTooltip
  ],
  template: `
    <td
      *ngFor="let col of cols"
      class="tsu-table-td"
      [colSpan]="col.id === ReportItemField.PLAN ? 2 : 1"
      [class.tsu-table-td--center]="col.align === 'center'"
    >
      <ng-container *ngIf="item" [ngSwitch]="col.id">
        <ng-container *ngSwitchCase="ReportItemField.ENTITY_NAME">
          <ng-container *ngIf="item.eventForm.controls.eventType as control">
            <ng-container *ngIf="item.events$ | async as eventTypes">
              <mat-form-field *ngIf="eventTypes.length !== 0" appearance="outline" style="width: 100%">
                <input [matTooltip]="getPropertyEvent(control.value, EventTypeProperties.name, eventTypes)"
                       [maxLength]="0" [readonly]="control.value !== null"
                       [formControl]="control"
                       [matAutocomplete]="auto"
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
            <input [formControl]="item.eventForm.controls.startDate" [matDatepicker]="dp1" matInput
                   style="display: none;">
            <div> {{ item.eventForm.controls.startDate.value | date: 'dd.MM.yyyy' }}</div>
            <mat-datepicker-toggle matSuffix [for]="dp1"></mat-datepicker-toggle>
            <mat-datepicker #dp1></mat-datepicker>
          </mat-form-field>
        </ng-container>

        <ng-container *ngSwitchCase="ReportItemField.END_DATE">
          <mat-form-field appearance="outline">
            <input [formControl]="item.eventForm.controls.endDate" [matDatepicker]="dp2" matInput
                   style="display: none;">
            <div> {{ item.eventForm.controls.endDate.value | date: 'dd.MM.yyyy' }}</div>
            <mat-datepicker-toggle matSuffix [for]="dp2"></mat-datepicker-toggle>
            <mat-datepicker #dp2></mat-datepicker>
          </mat-form-field>
        </ng-container>

        <ng-container *ngSwitchCase="ReportItemField.PLAN">
          <p *ngIf="item.eventForm.controls.eventType.valid" style="
                              font-size: 16px;
                              color: #830100;
                              background-color: #fbdfdf;
                              padding: 8px;
                              box-sizing: border-box;
                              border-radius: 8px;
                              border: 1px solid #830100;
                              font-weight: 500;
                              overflow: hidden;
                              white-space: nowrap;
                              text-overflow: ellipsis;
                              text-align: center;
        "
          >
            {{ getPropertyEvent(item.eventForm.controls.eventType.value, EventTypeProperties.description, (item.events$ | async) ?? []) }}
          </p>
        </ng-container>

        <ng-container *ngSwitchCase="ReportItemField.ACTIONS">
          <ng-container [ngTemplateOutlet]="actionsRef()"></ng-container>
        </ng-container>
      </ng-container>
    </td>
  `
})
export class EventFormTableRowComponent extends TableRowController<EventFormState, ReportItemField> {
  protected readonly ReportItemField = ReportItemField;
  protected readonly EventTypeProperties = EventTypeProperties;
  protected actualDescriptionEventType: string = "";

  protected displayFn(opts: IEventType[]) {
    return (id: string) => {
      // console.log(opts[1].id);
      const eventType = opts.find(v => v.id === id);
      return eventType ? eventType.name : '';
    }
  }

  protected getPropertyEvent(id: string | null, property: EventTypeProperties, opts: IEventType[]): string {
    if (!id) return "Выберите событие";
    let test = "";
    opts.forEach((opt) => {
      if (opt.id === id) {
        test = opt[property];
      }
    });

    if (property == this.EventTypeProperties.description) {
      this.actualDescriptionEventType = test;
    }

    return test;
  }
}

export enum ReportItemField {
  ACTIONS = 'ACTIONS',
  ENTITY_NAME = 'ENTITY_NAME',
  START_DATE = 'START_DATE',
  END_DATE = 'END_DATE',
  PLAN = 'PLAN',
  FACT = 'FACT'
}

export enum EventTypeProperties {
  name = 'name',
  description = 'description',
}
