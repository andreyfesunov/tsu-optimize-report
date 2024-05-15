import {Component, input, TemplateRef, ViewEncapsulation} from "@angular/core";
import {NgTemplateOutlet} from "@angular/common";

@Component({
  selector: 'app-modal-dialog',
  standalone: true,
  template: `
    <div class="modal-dialog">
      <div class="modal-dialog__title">
        <span>{{ title() }}</span>
      </div>

      <div class="modal-dialog__content">
        <ng-content></ng-content>
      </div>

      <div class="modal-dialog__actions">
        <ng-container *ngTemplateOutlet="actionsRef()"></ng-container>
      </div>
    </div>
  `,
  imports: [
    NgTemplateOutlet
  ],
  styleUrls: ['modal-dialog.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ModalDialogComponent {
  public readonly title = input.required<string>();
  public readonly actionsRef = input.required<TemplateRef<null>>();
}
