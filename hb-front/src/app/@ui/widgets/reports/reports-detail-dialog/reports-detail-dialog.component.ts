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
      [title]="'Заполнение отчёта'"
    >
      <app-reports-tabs [id]="id"></app-reports-tabs>
    </app-modal-dialog>
  `
})
export class ReportsDetailDialogComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) private readonly _dialogData: IReportsDetailDialogData,
  ) {
  }

  protected readonly id: string = this._dialogData.id;
}

export interface IReportsDetailDialogData {
  id: string;
}
