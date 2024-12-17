import {Injectable, inject} from "@angular/core";
import {Observable} from "rxjs";
import {IJob} from "@core/models";
import {HttpClient} from "@angular/common/http";
import { environment } from "src/environments/environment";

@Injectable({providedIn: "root"})
export class JobsService {
  private readonly _apiRoot = environment.apiRoot;
  private readonly _httpClient = inject(HttpClient);

  public list(): Observable<IJob[]> {
    return this._httpClient.get<IJob[]>(`${this._apiRoot}/Job`);
  }
}
