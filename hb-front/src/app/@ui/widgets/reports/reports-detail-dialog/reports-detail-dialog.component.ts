import {Component, Inject} from "@angular/core";
import {ModalDialogComponent, ReportsTabsComponent} from "@ui/widgets";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";

@Component({
  selector: 'app-reports-detail-dialog',
  standalone: true,
  imports: [
    ModalDialogComponent,
    ReportsTabsComponent
  ],
  template: `
    <app-modal-dialog
      [actionsRef]="actionsRef"
      [title]="'Заполнение отчёта'"
    >
      <app-reports-tabs></app-reports-tabs>
    </app-modal-dialog>

    <ng-template #actionsRef>

    </ng-template>
  `
})
export class ReportsDetailDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) private readonly _dialogData: ReportsDetailDialogData
  ) {
  }

  private readonly _id: string = this._dialogData.id;
}

export interface ReportsDetailDialogData {
  id: string;
}
