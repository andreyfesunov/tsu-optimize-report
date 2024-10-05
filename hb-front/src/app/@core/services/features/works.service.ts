import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IWork} from "@core/models";
import {Injectable, inject} from "@angular/core";

@Injectable({providedIn: "root"})
export class WorksService {
  private readonly _httpClient = inject(HttpClient);

  public getAll(): Observable<IWork[]> {
    return this._httpClient.get<IWork[]>('/api/Work');
  }
}
