import {Routes} from '@angular/router';
import { ReportsComponent } from './components/reports/reports.component';

export enum AppRoutes {
  USER_AREA = 'tsu',
  AUTH_AREA = 'auth'
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
  },
  {
    path: AppRoutes.AUTH_AREA,
    loadChildren: () => import('./processes/auth-area/auth-area.routes').then(m => m.routes)
  }
];
