import {Component} from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import {Observable} from "rxjs";
import {IWork} from "@core/models";
import {WorksService} from "@core/abstracts";
import {AsyncPipe, NgForOf, NgIf} from "@angular/common";
import {SpinnerComponent} from "@ui/widgets";
import {Spinner, withSpinner} from "@core/utils";

@Component({
  selector: 'app-reports-tabs',
  standalone: true,
  imports: [MatTabsModule, NgIf, AsyncPipe, NgForOf, SpinnerComponent],
  templateUrl: 'reports-tabs.component.html',
  styleUrl: 'reports-tabs.component.scss'
})
export class ReportsTabsComponent {
  constructor(
    private readonly _worksService: WorksService
  ) {
  }

  protected readonly spinner = new Spinner();

  protected readonly works$: Observable<IWork[]> = withSpinner(this._worksService.getAll(), this.spinner);
}
