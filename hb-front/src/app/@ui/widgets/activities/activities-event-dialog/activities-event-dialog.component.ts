import {Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ModalDialogActionComponent, ModalDialogComponent} from "@ui/widgets";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {MatAutocomplete, MatAutocompleteTrigger, MatOption} from "@angular/material/autocomplete";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {FormControl, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {IEventType} from "@core/models";
import {IActivitiesAssignEventRequest} from "@core/dtos";
import {concat, map, of, switchMap} from "rxjs";
import {EventTypesService} from "@core/services";

@Component({
  selector: "app-activities-event-dialog",
  standalone: true,
  imports: [
    ModalDialogComponent,
    ModalDialogActionComponent,
    AsyncPipe,
    MatAutocomplete,
    MatAutocompleteTrigger,
    MatFormField,
    MatInput,
    MatLabel,
    MatOption,
    NgForOf,
    NgIf,
    ReactiveFormsModule,
    FormsModule
  ],
  template: `
    <app-modal-dialog
      [title]="title"
      [actionsRef]="actionsRef"
    >
      <form (ngSubmit)="submit()">
        <mat-form-field *ngIf="events$ | async as eventTypes" appearance="outline">
          <mat-label>Мероприятие</mat-label>
          <input [formControl]="formControl" [matAutocomplete]="auto" matInput>

          <mat-autocomplete #auto="matAutocomplete" [displayWith]="displayFn(eventTypes)">
            <mat-option *ngFor="let eventType of eventTypes" [value]="eventType.id">{{ eventType.name }}</mat-option>
          </mat-autocomplete>
        </mat-form-field>
      </form>
    </app-modal-dialog>

    <ng-template #actionsRef>
      <app-modal-dialog-action [applyText]="'Назначить'" (apply)="submit()"></app-modal-dialog-action>
    </ng-template>
  `
})
export class ActivitiesEventDialogComponent {
  protected readonly formControl: FormControl<string> = new FormControl<string>('', {
    validators: [Validators.required],
    nonNullable: true
  });
  protected readonly events$ = this._eventsService.getAll().pipe(
    switchMap((events) => concat(of(this.formControl.value), this.formControl.valueChanges).pipe(
      map((value) => events.filter((event) => event.name.includes(value)))
    ))
  );
  protected readonly title = `Назначить событие активности "${this._dialogData.activityName}"`;

  constructor(
    private readonly _eventsService: EventTypesService,
    private readonly _dialogRef: MatDialogRef<ActivitiesEventDialogComponent, IActivitiesAssignEventRequest>,
    @Inject(MAT_DIALOG_DATA) private readonly _dialogData: IActivitiesEventDialogData
  ) {
  }

  protected displayFn(opts: IEventType[]) {
    return (id: string) => {
      const eventType = opts.find(v => v.id === id);
      return eventType ? eventType.name : '';
    }
  }

  protected submit(): void {
    if (this.formControl.invalid) {
      return this.formControl.markAllAsTouched();
    }

    const request: IActivitiesAssignEventRequest = {
      activityId: this._dialogData.activityId,
      eventTypeId: this.formControl.value
    }

    this._dialogRef.close(request);
  }
}

export interface IActivitiesEventDialogData {
  activityId: string;
  activityName: string;
}
