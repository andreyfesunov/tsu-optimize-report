import {IPaginationRequest, IStateAssignRequest, IStateCreateRequest} from "@core/dtos";
import {concat, Observable, of, Subject, switchMap, tap} from "rxjs";
import {IPagination, IState} from "@core/models";
import {HttpClient} from "@angular/common/http";
import {Injectable, inject} from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({providedIn: "root"})
export class StatesService {
  private readonly _apiRoot = environment.apiRoot;
  private readonly _httpClient = inject(HttpClient);
  private readonly _reload$: Subject<void> = new Subject<void>();

  public search(dto: IPaginationRequest): Observable<IPagination<IState>> {
    return concat(of(0), this._reload$).pipe(
      switchMap(() => this._httpClient.post<IPagination<IState>>(`${this._apiRoot}/State/search`, dto))
    );
  }

  public assign(dto: IStateAssignRequest): Observable<boolean> {
    return this._httpClient.post<boolean>(`${this._apiRoot}/State/assign`, dto).pipe(
      tap(() => this._reload$.next())
    );
  }

  public create(dto: IStateCreateRequest): Observable<string> {
    return this._httpClient.post<string>(`${this._apiRoot}/State`, dto).pipe(
      tap(() => this._reload$.next())
    )
  }
}
