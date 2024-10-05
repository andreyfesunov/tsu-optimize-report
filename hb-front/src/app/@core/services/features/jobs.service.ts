import {Injectable, inject} from "@angular/core";
import {Observable} from "rxjs";
import {IJob} from "@core/models";
import {HttpClient} from "@angular/common/http";

@Injectable({providedIn: "root"})
export class JobsService {
  private readonly _httpClient = inject(HttpClient);

  public list(): Observable<IJob[]> {
    return this._httpClient.get<IJob[]>('/api/Job');
  }
}
