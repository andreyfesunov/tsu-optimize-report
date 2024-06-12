import {Component} from "@angular/core";
import {ContentComponent, ReportFormComponent, ReportsFirstHalfTableComponent, ScrollableComponent} from "@ui/widgets";
import {ActivatedRoute} from "@angular/router";
import {map} from "rxjs";
import {ParamsRoutes} from "@core/models";
import {exists} from "@core/utils";
import {AsyncPipe, NgIf} from "@angular/common";

@Component({
  selector: 'app-report-detail',
  standalone: true,
  imports: [
    ContentComponent,
    ReportFormComponent,
    AsyncPipe,
    NgIf,
    ScrollableComponent,
    ReportsFirstHalfTableComponent
  ],
  template:
    `
      <app-content>
        <div *ngIf="id$ | async as id" class="report-detail host-class">

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
export class ReportDetailComponent {
  constructor(
    private readonly _route: ActivatedRoute
  ) {
  }

  protected readonly id$ = this._route.paramMap.pipe(
    map((v) => v.get(ParamsRoutes.ID)),
    exists()
  )
}
