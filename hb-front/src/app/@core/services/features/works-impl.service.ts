import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IWork} from "@core/models";
import {Injectable} from "@angular/core";
import {WorksService} from "@core/abstracts";

@Injectable()
export class WorksImplService extends WorksService {
  constructor(private readonly _httpClient: HttpClient) {
    super();
  }

  public getAll(): Observable<IWork[]> {
    return this._httpClient.get<IWork[]>('/api/Work/getAll');
  }
}
