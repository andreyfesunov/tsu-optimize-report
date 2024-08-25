import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IWork } from "@core/models";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: "root" })
export class WorksService {
  constructor(private readonly _httpClient: HttpClient) {
  }

  public getAll(): Observable<IWork[]> {
    return this._httpClient.get<IWork[]>('/api/Work');
  }
}
