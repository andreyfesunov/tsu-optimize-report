import {Routes} from '@angular/router';
import { ReportsComponent } from './components/reports/reports.component';

export enum AppRoutes {
  USER_AREA = 'user-area'
}

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'reports'
  },
  {
    path: 'reports',
    component: ReportsComponent,
    // canActivate: [authGuard],  
},
  {
    path: AppRoutes.USER_AREA,
    loadChildren: () => import('./processes/work-area/work-area.routes').then(m => m.routes)
  }
];
