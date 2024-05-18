import {Component} from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';
import {Observable} from "rxjs";
import {IInstitute} from "@core/models";
import {InstitutesService} from "@core/abstracts";
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
    private readonly _instituteService: InstitutesService
  ) {
  }

  protected readonly spinner = new Spinner();

  protected readonly institutes$: Observable<IInstitute[]> = withSpinner(this._instituteService.getAll(), this.spinner);
}
