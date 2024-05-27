import {Component, Inject} from "@angular/core";
import {ModalDialogComponent, ReportFormComponent} from "@ui/widgets";
import {MAT_DIALOG_DATA} from "@angular/material/dialog";

@Component({
  selector: 'app-reports-detail-dialog',
  standalone: true,
  imports: [
    ModalDialogComponent,
    ReportFormComponent
  ],
  template: `
    <app-modal-dialog
      [title]="'Заполнение отчёта'"
    >
      <app-report-form [id]="id"></app-report-form>
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
