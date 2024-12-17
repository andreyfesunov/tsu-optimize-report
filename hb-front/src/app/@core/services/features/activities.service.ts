import {Injectable, inject} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IActivity} from "@core/models";
import { environment } from "src/environments/environment";

@Injectable({providedIn: "root"})
export class ActivitiesService {
  private readonly _apiRoot = environment.apiRoot;
  private readonly _httpClient = inject(HttpClient);

  public getAll(): Observable<readonly IActivity[]> {
    return this._httpClient.get<readonly IActivity[]>(`${this._apiRoot}/Activity`);
  }
}
