import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IActivity} from "@core/models";

@Injectable({providedIn: "root"})
export class ActivitiesService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
  }

  public getAll(): Observable<readonly IActivity[]> {
    return this._httpClient.get<readonly IActivity[]>('/api/Activity');
  }
}
