import {Component, input} from "@angular/core";
import {CommentFormState} from "@core/states";
import {ITableColumn} from "@core/models";
import {ReportItemField} from "@ui/widgets";
import {AsyncPipe, NgForOf, NgIf, NgSwitch, NgSwitchCase} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatFormField} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";

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
          <mat-form-field appearance="outline">
            <input [formControl]="state().form.controls.content" type="textarea" matInput>
          </mat-form-field>
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
export class CommentFormTableRowComponent {
  public readonly state = input.required<CommentFormState>();
  public readonly defaultCols = input.required<ITableColumn<ReportItemField>[]>();

  protected readonly ReportItemField = ReportItemField;
}
