import {Routes} from '@angular/router';

export enum AppRoutes {
  USER_AREA = 'user-area'
}

export const routes: Routes = [
  {
    path: AppRoutes.USER_AREA,
    loadChildren: () => import('./processes/work-area/work-area.routes').then(m => m.routes)
  }
];
