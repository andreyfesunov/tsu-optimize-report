import {Observable} from "rxjs";
import {IRecord} from "@core/models";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable({providedIn: 'root'})
export class RecordsService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
  }

  public get(reportId: string): Observable<IRecord[]> {
    return this._httpClient.get<IRecord[]>(`/api/Record/${reportId}`);
  }
}
