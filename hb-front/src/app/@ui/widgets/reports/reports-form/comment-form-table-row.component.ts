import {Component} from "@angular/core";
import {CommentFormState} from "@core/states";
import {ReportItemField} from "@ui/widgets";
import {AsyncPipe, NgForOf, NgIf, NgSwitch, NgSwitchCase, NgTemplateOutlet} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatFormField} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {TableRowController} from "@core/controllers";
import {MatTooltip} from "@angular/material/tooltip";

@Component({
  selector: "tr[app-comment-form-table-row]",
  standalone: true,
  imports: [
    AsyncPipe,
    FormsModule,
    MatAutocomplete,
    MatAutocompleteTrigger,
    MatFormField,
    MatInput,
    MatOption,
    NgForOf,
    NgIf,
    NgSwitchCase,
    NgSwitch,
    ReactiveFormsModule,
    NgTemplateOutlet,
    MatTooltip
  ],
  template: `
    <td
      *ngFor="let col of cols"
      class="tsu-table-td"
      [class.tsu-table-td--center]="col.align === 'center'"
    >
      <ng-container *ngIf="item" [ngSwitch]="col.id">
        <ng-container *ngSwitchCase="ReportItemField.ENTITY_NAME">
          <mat-form-field appearance="outline">
            <input [matTooltip]="item.form.controls.content.value" [formControl]="item.form.controls.content"
                   type="textarea" matInput>
          </mat-form-field>
        </ng-container>

        <ng-container *ngSwitchCase="ReportItemField.PLAN">
          <mat-form-field appearance="outline">
            <input [formControl]="item.form.controls.plan"
                   [min]="0"
                   type="number"
                   matInput>
          </mat-form-field>
        </ng-container>

        <ng-container *ngSwitchCase="ReportItemField.FACT">
          <mat-form-field appearance="outline">
            <input [formControl]="item.form.controls.fact"
                   [min]="0"
                   type="number"
                   matInput>
          </mat-form-field>
        </ng-container>

        <ng-container *ngSwitchCase="ReportItemField.ACTIONS">
          <ng-container [ngTemplateOutlet]="actionsRef()"></ng-container>
        </ng-container>
      </ng-container>
    </td>
  `
})
export class CommentFormTableRowComponent extends TableRowController<CommentFormState, ReportItemField> {
  protected readonly ReportItemField = ReportItemField;
}
