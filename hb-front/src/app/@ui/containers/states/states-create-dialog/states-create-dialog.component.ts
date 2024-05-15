import {Component} from "@angular/core";
import {ModalDialogActionComponent, ModalDialogComponent, StatesCreateDialogFormComponent} from "@ui/widgets";
import {Subject} from "rxjs";

@Component({
  selector: 'app-states-create-dialog',
  standalone: true,
  imports: [
    ModalDialogComponent,
    ModalDialogActionComponent,
    StatesCreateDialogFormComponent
  ],
  template: `
    <app-modal-dialog
      [title]="'Создание ставки'"
      [actionsRef]="actionsRef"
    >
      <app-states-create-dialog-form [jobs]="[]"></app-states-create-dialog-form>
    </app-modal-dialog>

    <ng-template #actionsRef>
      <app-modal-dialog-action (apply)="submit$.next()"></app-modal-dialog-action>
    </ng-template>
  `
})
export class StatesCreateDialogComponent {
  protected readonly submit$ = new Subject<void>();
}
