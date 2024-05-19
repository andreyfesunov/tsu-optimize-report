import {Component} from "@angular/core";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {FormControl, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatFormField, MatLabel, MatPrefix} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {ModalDialogActionComponent, ModalDialogComponent} from "@ui/widgets";
import {MatIconButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-reports-start-dialog',
  standalone: true,
  imports: [
    AsyncPipe,
    FormsModule,
    MatAutocomplete,
    MatAutocompleteTrigger,
    MatFormField,
    MatInput,
    MatLabel,
    MatOption,
    ModalDialogActionComponent,
    ModalDialogComponent,
    NgForOf,
    NgIf,
    ReactiveFormsModule,
    MatIconButton,
    MatPrefix,
    MatIcon
  ],
  template: `
    <app-modal-dialog
      [title]="'Прикрепите первую половину'"
      [actionsRef]="actionsRef"
    >
      <form (ngSubmit)="submit()">
        <mat-form-field appearance="outline">
          <mat-label>Файл</mat-label>
          <button mat-icon-button matPrefix (click)="$event.preventDefault(); file_input.click()">
            <mat-icon>attach_file</mat-icon>
          </button>
          <input type="text" readonly [formControl]="displayControl" matInput>
          <input type="file" hidden accept=".xls, .xlsx" #file_input (change)="change(file_input.files)">
        </mat-form-field>
      </form>
    </app-modal-dialog>

    <ng-template #actionsRef>
      <app-modal-dialog-action [applyText]="'Начать'" (apply)="submit()"
                               [applyDisabled]="fileControl.invalid"></app-modal-dialog-action>
    </ng-template>
  `
})
export class ReportsStartDialogComponent {
  constructor(
    private readonly _dialogRef: MatDialogRef<ReportsStartDialogComponent, FormData>
  ) {
  }

  protected readonly fileControl: FormControl<File | null> = new FormControl<File | null>(null, Validators.required);
  protected readonly displayControl: FormControl<string> = new FormControl<string>('', {nonNullable: true});

  protected change(files: FileList): void {
    if (files.length === 0) return;

    const file = files[0];
    this.displayControl.setValue(file.name);
    this.fileControl.setValue(file);
  }

  protected submit(): void {
    if (this.fileControl.invalid || this.fileControl.value === null) {
      return this.fileControl.markAllAsTouched();
    }

    const formData = new FormData();
    formData.append("file", this.fileControl.value);

    this._dialogRef.close(formData);
  }
}
