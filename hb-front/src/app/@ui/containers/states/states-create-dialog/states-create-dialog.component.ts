import {Component} from "@angular/core";
import {ModalDialogActionComponent, ModalDialogComponent, StatesCreateDialogFormComponent} from "@ui/widgets";
import {Subject} from "rxjs";
import {JobsService} from "@core/abstracts";
import {AsyncPipe, NgIf} from "@angular/common";

@Component({
  selector: 'app-states-create-dialog',
  standalone: true,
  imports: [
    ModalDialogComponent,
    ModalDialogActionComponent,
    StatesCreateDialogFormComponent,
    AsyncPipe,
    NgIf
  ],
  template: `
    <app-modal-dialog
      [title]="'Создание ставки'"
      [actionsRef]="actionsRef"
    >
      <app-states-create-dialog-form *ngIf="jobs$ | async as jobs" [jobs]="jobs"></app-states-create-dialog-form>
    </app-modal-dialog>

    <ng-template #actionsRef>
      <app-modal-dialog-action (apply)="submit$.next()"></app-modal-dialog-action>
    </ng-template>
  `
})
export class StatesCreateDialogComponent {
  constructor(private readonly _jobsService: JobsService) {
  }

  protected readonly jobs$ = this._jobsService.list();

  protected readonly submit$ = new Subject<void>();
}
