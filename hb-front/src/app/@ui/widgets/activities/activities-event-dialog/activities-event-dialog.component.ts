import {Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {ModalDialogActionComponent, ModalDialogComponent} from "@ui/widgets";

@Component({
  selector: "app-activities-event-dialog",
  standalone: true,
  imports: [
    ModalDialogComponent,
    ModalDialogActionComponent
  ],
  template: `
    <app-modal-dialog
      [title]="title"
      [actionsRef]="actionsRef"
    >
    </app-modal-dialog>

    <ng-template #actionsRef>
      <app-modal-dialog-action [applyText]="'Назначить'"></app-modal-dialog-action>
    </ng-template>
  `
})
export class ActivitiesEventDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) private readonly _dialogData: IActivitiesEventDialogData) {
  }

  protected readonly title = `Назначить событие активности "${this._dialogData.activityName}"`;
}

export interface IActivitiesEventDialogData {
  activityId: string;
  activityName: string;
}
