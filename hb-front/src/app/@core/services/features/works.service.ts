import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IWork} from "@core/models";
import {Injectable, inject} from "@angular/core";
import { environment } from "src/environments/environment";
@Injectable({providedIn: "root"})
export class WorksService {
  private readonly _apiRoot = environment.apiRoot;
  private readonly _httpClient = inject(HttpClient);

  public getAll(): Observable<IWork[]> {
    return this._httpClient.get<IWork[]>(`${this._apiRoot}/Work`);
  }
}
