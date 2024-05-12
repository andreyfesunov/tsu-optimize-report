import {Component, input} from "@angular/core";
import {IPagination} from "@core/models";
import {MatIconButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";

@Component({
  selector: 'app-paginator',
  standalone: true,
  styleUrls: ['paginator.component.scss'],
  template: `
    <div class="paginator" [class.paginator--hidden]="paginationDisabled">
      <div class="paginator-item">
        <div class="paginator-item__label">{{ startItemNumber }} - {{ endItemNumber }},
          страница {{ page().pageNumber }}
          из {{ page().totalPages }}
        </div>
        <div class="paginator-item__actions">
          <button mat-icon-button [disabled]="prevDisabled">
            <mat-icon>chevron_left</mat-icon>
          </button>

          <button mat-icon-button [disabled]="nextDisabled">
            <mat-icon>chevron_right</mat-icon>
          </button>
        </div>
      </div>
    </div>
  `,
  imports: [
    MatIconButton,
    MatIcon
  ]
})
export class PaginatorComponent {
  public readonly page = input.required<IPagination<unknown>>();

  protected get paginationDisabled(): boolean {
    return this.page().totalPages === 1;
  }

  protected get startItemNumber(): number {
    return (this.page().pageNumber - 1) * this.page().pageSize + 1;
  }

  protected get endItemNumber(): number {
    return (this.page().pageNumber - 1) * this.page().pageSize + this.page().entities.length;
  }

  protected get prevDisabled(): boolean {
    return this.page().pageNumber === 1;
  }

  protected get nextDisabled(): boolean {
    return this.page().pageNumber === this.page().totalPages;
  }
}
