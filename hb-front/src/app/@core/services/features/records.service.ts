import {Observable} from "rxjs";
import {IRecord} from "@core/models";
import {HttpClient} from "@angular/common/http";
import {Injectable, inject} from "@angular/core";

@Injectable({providedIn: 'root'})
export class RecordsService {
  private readonly _httpClient = inject(HttpClient);

  public get(reportId: string): Observable<IRecord[]> {
    return this._httpClient.get<IRecord[]>(`/api/Record/${reportId}`);
  }
}
