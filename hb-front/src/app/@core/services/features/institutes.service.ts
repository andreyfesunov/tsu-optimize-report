import {Observable} from "rxjs";
import {IInstitute} from "@core/models";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable({providedIn: "root"})
export class InstitutesService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
  }

  getAll(): Observable<IInstitute[]> {
    return this._httpClient.get<IInstitute[]>('/api/Institute/getAll');
  }
}
