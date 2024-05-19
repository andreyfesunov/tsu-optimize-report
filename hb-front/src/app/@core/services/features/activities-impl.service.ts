import {ActivitiesService} from "@core/abstracts";
import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IActivity} from "@core/models";

@Injectable()
export class ActivitiesImplService extends ActivitiesService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
    super();
  }

  public getAll(): Observable<IActivity[]> {
    return this._httpClient.get<IActivity[]>('/api/Activity/getAll');
  }
}
