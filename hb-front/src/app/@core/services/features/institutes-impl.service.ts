import {InstitutesService} from "@core/abstracts";
import {Observable} from "rxjs";
import {IInstitute} from "@core/models";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export class InstitutesImplService extends InstitutesService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
    super();
  }

  getAll(): Observable<IInstitute[]> {
    return this._httpClient.get<IInstitute[]>('/api/Institute/getAll');
  }
}
