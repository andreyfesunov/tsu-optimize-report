import {Component, inject} from "@angular/core";
import {ContentComponent, ReportFormComponent, ReportsFirstHalfTableComponent, ScrollableComponent} from "@ui/widgets";
import {ActivatedRoute} from "@angular/router";
import {map} from "rxjs";
import {ParamsRoutes} from "@core/models";
import {exists} from "@core/utils";
import {AsyncPipe, NgIf} from "@angular/common";
import {MatButton, MatIconButton} from "@angular/material/button";
import {MatIcon} from "@angular/material/icon";
import {ReportsService} from "@core/services";
import {SubscriptionController} from "@core/controllers";
import FileSaver from 'file-saver';

@Component({
  selector: 'app-report-detail',
  standalone: true,
  imports: [
    ContentComponent,
    ReportFormComponent,
    AsyncPipe,
    NgIf,
    ScrollableComponent,
    ReportsFirstHalfTableComponent,
    MatButton,
    MatIconButton,
    MatIcon
  ],
  template:
    `
      <app-content *ngIf="id$ | async as id" class="report-detail">
        <div class="report-detail-actions">
          <button mat-flat-button (click)="export(id)">
            <mat-icon>download</mat-icon>
            <span>Скачать</span>
          </button>
        </div>

        <div class="report-detail-tabs host-class">

          <app-scrollable
            class="host-class report-table"
            [horizontalScroll]="true"
          >
            <app-scrollable
              class="host-class"
              [horizontalScroll]="true"
            >
              <app-reports-first-half-table
                [reportId]="id"
              ></app-reports-first-half-table>
            </app-scrollable>
          </app-scrollable>

          <app-scrollable
            class="host-class report-table"
            [horizontalScroll]="true"
          >
            <app-report-form
              [id]="id"
            ></app-report-form>
          </app-scrollable>

        </div>
      </app-content>
    `,
  host: {class: 'host-class'},
  styleUrls: ['report-detail.component.scss']
})
export class ReportDetailComponent extends SubscriptionController {
  private readonly _route = inject(ActivatedRoute);
  private readonly _reportsService = inject(ReportsService);

  protected readonly id$ = this._route.paramMap.pipe(
    map((v) => v.get(ParamsRoutes.ID)),
    exists()
  )

  protected export(id: string): void {
    this.subscription.add(
      this._reportsService.export(id).subscribe((data) => {
        FileSaver.saveAs(new Blob([data]), "ind-plan.xls");
      })
    )
  }
}
