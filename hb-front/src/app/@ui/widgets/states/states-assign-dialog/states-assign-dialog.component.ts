import {Component, inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {FormControl, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {ModalDialogActionComponent, ModalDialogComponent} from "@ui/widgets";
import {concat, map, of, switchMap} from "rxjs";
import {IUser} from "@core/models";
import {IStateAssignRequest} from "@core/dtos";
import {UsersService} from "@core/services";

@Component({
  selector: 'app-states-assign-dialog',
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
    ReactiveFormsModule
  ],
  template: `
    <app-modal-dialog
      [title]="'Назначить ставку'"
      [actionsRef]="actionsRef"
    >
      <form (ngSubmit)="submit()">
        <mat-form-field *ngIf="users$ | async as users" appearance="outline">
          <mat-label>Пользователь</mat-label>
          <input [formControl]="formControl" [matAutocomplete]="auto" matInput>

          <mat-autocomplete #auto="matAutocomplete" [displayWith]="displayFn(users)">
            <mat-option *ngFor="let user of users" [value]="user.id">{{ displayFn(users)(user.id) }}</mat-option>
          </mat-autocomplete>
        </mat-form-field>
      </form>
    </app-modal-dialog>

    <ng-template #actionsRef>
      <app-modal-dialog-action [applyText]="'Назначить'" (apply)="submit()"></app-modal-dialog-action>
    </ng-template>
  `
})
export class StatesAssignDialogComponent {
  private readonly _usersService = inject(UsersService);
  private readonly _dialogRef = inject<MatDialogRef<StatesAssignDialogComponent, IStateAssignRequest>>(MatDialogRef);
  private readonly _dialogData = inject<IStatesAssignDialogData>(MAT_DIALOG_DATA);

  protected readonly formControl: FormControl<string> = new FormControl<string>('', {
    validators: [Validators.required],
    nonNullable: true
  });
  protected readonly users$ = this._usersService.getAll().pipe(
    switchMap((users) => concat(of(this.formControl.value), this.formControl.valueChanges).pipe(
      map((value) => users.filter((user) => `${user.firstname} ${user.lastname}`.includes(value)))
    ))
  );

  protected displayFn(opts: IUser[]) {
    return (id: string) => {
      const user = opts.find(v => v.id === id);
      return user ? `${user.firstname} ${user.lastname}` : '';
    }
  }

  protected submit(): void {
    if (this.formControl.invalid) {
      return this.formControl.markAllAsTouched();
    }

    const request: IStateAssignRequest = {
      stateId: this._dialogData.stateId,
      userId: this.formControl.value
    }

    this._dialogRef.close(request);
  }
}

export interface IStatesAssignDialogData {
  stateId: string;
}
