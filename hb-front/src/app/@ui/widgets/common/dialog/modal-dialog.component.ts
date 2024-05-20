import {Component, input, TemplateRef, ViewEncapsulation} from "@angular/core";
import {NgIf, NgTemplateOutlet} from "@angular/common";
import {ScrollableComponent} from "@ui/widgets";

@Component({
  selector: 'app-modal-dialog',
  standalone: true,
  template: `
    <div class="modal-dialog">
      <div class="modal-dialog__title">
        <span>{{ title() }}</span>
      </div>

      <app-scrollable class="modal-dialog__content">
        <ng-content></ng-content>
      </app-scrollable>

      <div *ngIf="actionsRef() as actionsRef" class="modal-dialog__actions">
        <ng-container *ngTemplateOutlet="actionsRef"></ng-container>
      </div>
    </div>
  `,
  imports: [
    NgTemplateOutlet,
    NgIf,
    ScrollableComponent
  ],
  styleUrls: ['modal-dialog.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ModalDialogComponent {
  public readonly title = input.required<string>();
  public readonly actionsRef = input<TemplateRef<null> | null>(null);
}
