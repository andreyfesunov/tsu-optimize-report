import {Component} from '@angular/core';
import {MatTabsModule} from '@angular/material/tabs';

@Component({
    selector: 'app-reports-tabs',
    standalone: true,
    imports: [MatTabsModule],
    templateUrl: './reports-tabs.component.html',
    styleUrl: './reports-tabs.component.scss'
})
export class ReportsTabsComponent {

}
