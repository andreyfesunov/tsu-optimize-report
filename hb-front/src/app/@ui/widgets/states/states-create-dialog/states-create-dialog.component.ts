import {Component} from "@angular/core";
import {MatButton} from "@angular/material/button";
import {MatFormField, MatLabel, MatSuffix} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {IJob} from "@core/models";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {IStateCreateRequest} from "@core/dtos";
import {JobsService} from "@core/abstracts";
import {ModalDialogActionComponent, ModalDialogComponent} from "@ui/widgets";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-states-create-dialog',
  standalone: true,
  imports: [
    MatButton,
    MatFormField,
    MatLabel,
    MatInput,
    ReactiveFormsModule,
    MatAutocompleteTrigger,
    MatAutocomplete,
    MatOption,
    NgForOf,
    NgIf,
    FormsModule,
    MatDatepickerToggle,
    MatSuffix,
    MatDatepicker,
    MatDatepickerInput,
    AsyncPipe,
    ModalDialogActionComponent,
    ModalDialogComponent
  ],
  template: `
    <app-modal-dialog
      [title]="'Создание ставки'"
      [actionsRef]="actionsRef"
    >
      <form (ngSubmit)="submit()">
        <mat-form-field *ngIf="jobs$ | async as jobs" appearance="outline">
          <mat-label>Должность</mat-label>
          <input [formControl]="form.controls.job" [matAutocomplete]="auto" matInput>

          <mat-autocomplete #auto="matAutocomplete" [displayWith]="displayFn(jobs)">
            <mat-option *ngFor="let job of jobs" [value]="job.id">{{ job.name }}</mat-option>
          </mat-autocomplete>
        </mat-form-field>

        <mat-form-field appearance="outline">
          <mat-label>Количество ставок</mat-label>
          <input [formControl]="form.controls.count" type="number" matInput>
        </mat-form-field>

        <mat-form-field appearance="outline">
          <mat-label>Количество рабочих часов</mat-label>
          <input [formControl]="form.controls.hours" type="number" matInput>
        </mat-form-field>

        <mat-form-field appearance="outline">
          <mat-label>Дата начала</mat-label>
          <input [formControl]="form.controls.startDate" [matDatepicker]="dp1" matInput>
          <mat-datepicker-toggle matSuffix [for]="dp1"></mat-datepicker-toggle>
          <mat-datepicker #dp1></mat-datepicker>
        </mat-form-field>

        <mat-form-field appearance="outline">
          <mat-label>Дата окончания</mat-label>
          <input [formControl]="form.controls.endDate" [matDatepicker]="dp2" matInput [disabled]="true">
          <mat-datepicker-toggle matSuffix [for]="dp2"></mat-datepicker-toggle>
          <mat-datepicker #dp2></mat-datepicker>
        </mat-form-field>
      </form>
    </app-modal-dialog>

    <ng-template #actionsRef>
      <app-modal-dialog-action (apply)="submit()"></app-modal-dialog-action>
    </ng-template>
  `
})
export class StatesCreateDialogComponent {
  constructor(
    private readonly _jobsService: JobsService,
    private readonly _dialogRef: MatDialogRef<StatesCreateDialogComponent>
  ) {
  }

  protected readonly jobs$ = this._jobsService.list();

  protected displayFn(opts: IJob[]) {
    return (id: string) => {
      const job = opts.find(v => v.id === id);
      return job ? job.name : '';
    }
  }

  protected readonly form: FormGroup<IStatesDialogForm> = new FormGroup<IStatesDialogForm>({
    hours: new FormControl<number>(1485, {nonNullable: true, validators: [Validators.min(1), Validators.required]}),
    count: new FormControl<number>(1, {nonNullable: true, validators: [Validators.min(0), Validators.required]}),
    job: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
    startDate: new FormControl<Date>(new Date(), {nonNullable: true, validators: [Validators.required]}),
    endDate: new FormControl<Date>(this._endDate, {
      nonNullable: true,
      validators: [Validators.required]
    }),
  });

  private get _endDate(): Date {
    const endDate = new Date();
    endDate.setDate(endDate.getDate() + 30);
    return endDate;
  }

  protected submit(): void {
    if (this.form.invalid) {
      return this.form.markAllAsTouched();
    }

    const request: IStateCreateRequest = {
      count: this.form.controls.count.value,
      hours: this.form.controls.hours.value,
      startDate: this.form.controls.startDate.value,
      endDate: this.form.controls.endDate.value,
      jobId: this.form.controls.job.value
    }

    this._dialogRef.close(request);
  }
}

interface IStatesDialogForm {
  hours: FormControl<number>;
  count: FormControl<number>;
  startDate: FormControl<Date>;
  endDate: FormControl<Date>;
  job: FormControl<string>;
}
