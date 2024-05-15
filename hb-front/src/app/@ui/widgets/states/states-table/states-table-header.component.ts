import {Component, output} from "@angular/core";
import {TableHeaderComponent} from "@ui/widgets";
import {MatButton} from "@angular/material/button";

@Component({
  selector: 'app-states-table-header',
  standalone: true,
  template: `
    <app-table-header>
      <button mat-flat-button (click)="create.emit()">Создать</button>
    </app-table-header>
  `,
  imports: [
    TableHeaderComponent,
    MatButton
  ]
})
export class StatesTableHeaderComponent {
  public readonly create = output<void>();
}
