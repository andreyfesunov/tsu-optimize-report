import {Observable} from "rxjs";
import {IInstitute} from "@core/models";
import {HttpClient} from "@angular/common/http";
import {Injectable, inject} from "@angular/core";

@Injectable({providedIn: "root"})
export class InstitutesService {
  private readonly _httpClient = inject(HttpClient);

  getAll(): Observable<IInstitute[]> {
    return this._httpClient.get<IInstitute[]>('/api/Institute/getAll');
  }
}
