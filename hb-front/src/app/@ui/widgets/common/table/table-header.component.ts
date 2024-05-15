import {Component} from "@angular/core";

@Component({
  selector: 'app-table-header',
  standalone: true,
  template: `
    <div class="table-header">
      <ng-content></ng-content>
    </div>
  `,
  styles: `
    @use "../../../../../scss/shared/variables";

    .table-header {
      display: flex;

      gap: variables.$padding-md;
      padding-bottom: variables.$padding-md;
    }
  `
})
export class TableHeaderComponent {

}
