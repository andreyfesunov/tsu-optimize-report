import { Component, Input } from "@angular/core";
import { IState } from "@core/dtos";
import { NgForOf } from "@angular/common";
import { DatePipe } from "@angular/common";

@Component({
  selector: 'tr[app-users-table-state-row]',
  standalone: true,
  imports: [NgForOf, DatePipe],
  template: `
    <td [attr.colspan]="colsCount" class="state-cell">
      <div class="states-grid">
        <div *ngFor="let state of states" class="state-card">
          <div class="state-row">
            <strong>Должность:</strong> {{ state.job.name }}
          </div>
          <div class="state-row">
            <strong>Кафедра:</strong> {{ state.department.name }}
          </div>
          <div class="state-row">
            <strong>Институт:</strong> {{ state.department.institute.name }}
          </div>
          <div class="state-row">
            <strong>Часы:</strong> {{ state.hours }}
          </div>
          <div class="state-row">
            <strong>Дата начала:</strong> {{ state.startDate | date:'dd.MM.yyyy' }}
          </div>
          <div class="state-row">
            <strong>Дата окончания:</strong> {{ state.endDate | date:'dd.MM.yyyy' }}
          </div>
        </div>
      </div>
    </td>
  `,
  styles: [`
    .states-grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
      gap: 16px;
      padding: 16px;
    }
    .state-card {
      background: #fffcfc;
      border-radius: 8px;
      padding: 16px;
      box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    }
    .state-row {
      margin-bottom: 8px;
    }
    .state-row:last-child {
      margin-bottom: 0;
    }
  `]
})
export class UsersTableStateRowComponent {
  @Input() states: IState[] = [];
  @Input() colsCount: number = 1;
} 