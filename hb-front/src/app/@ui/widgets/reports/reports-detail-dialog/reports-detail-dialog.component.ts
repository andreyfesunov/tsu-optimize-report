import {Component, inject} from "@angular/core";
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
  private readonly _dialogData = inject<IReportsDetailDialogData>(MAT_DIALOG_DATA);
  protected readonly id: string = this._dialogData.id;
}

export interface IReportsDetailDialogData {
  id: string;
}
