import { Observable } from 'rxjs';
import { IRecord, SemesterEnum } from '@core/models';
import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class RecordsService {
  private readonly _apiRoot = environment.apiRoot;
  private readonly _httpClient = inject(HttpClient);

  public get(
    reportId: string,
    semesterId: SemesterEnum,
  ): Observable<IRecord[]> {
    return this._httpClient.get<IRecord[]>(
      `${this._apiRoot}/Record/${reportId}/${semesterId.toString()}`,
    );
  }
}
