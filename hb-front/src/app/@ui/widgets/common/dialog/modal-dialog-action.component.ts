import {Component, inject, input, output} from "@angular/core";
import {MatButton} from "@angular/material/button";
import {DialogRef} from "@angular/cdk/dialog";

@Component({
  selector: 'app-modal-dialog-action',
  standalone: true,
  template: `
    <div class="modal-dialog-action">
      <!-- Can extend -->

      <button mat-button
              [disabled]="cancelDisabled()"
              (click)="cancel()"
      >
        Отмена
      </button>

      <button mat-flat-button
              [disabled]="applyDisabled()"
              (click)="apply.emit()"
      >
        {{ applyText() }}
      </button>
    </div>
  `,
  imports: [
    MatButton
  ],
  styleUrls: ['modal-dialog-action.component.scss']
})
export class ModalDialogActionComponent {
  private readonly _dialogRef = inject(DialogRef);

  public readonly cancelDisabled = input<boolean>(false);
  public readonly applyDisabled = input<boolean>(false);
  public readonly applyText = input<string>('Создать');
  public readonly apply = output();

  protected readonly cancel = () => this._dialogRef.close();
}
