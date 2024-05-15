import {JobsService} from "@core/abstracts";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {IJob} from "@core/models";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class JobsImplService extends JobsService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
    super();
  }

  public list(): Observable<IJob[]> {
    return this._httpClient.get<IJob[]>('/api/Job/getAll');
  }
}
