import {Observable} from "rxjs";
import {IInstitute} from "@core/models";
import {HttpClient} from "@angular/common/http";
import {Injectable, inject} from "@angular/core";
import { environment } from "src/environments/environment";
@Injectable({providedIn: "root"})
export class InstitutesService {
  private readonly _apiRoot = environment.apiRoot;
  private readonly _httpClient = inject(HttpClient);

  public getAll(): Observable<IInstitute[]> {
    return this._httpClient.get<IInstitute[]>(`${this._apiRoot}/Institute/getAll`);
  }
}
