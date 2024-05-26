import {IPaginationRequest, IStateAssignRequest, IStateCreateRequest} from "@core/dtos";
import {concat, Observable, of, Subject, switchMap, tap} from "rxjs";
import {IPagination, IState} from "@core/models";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable({providedIn: "root"})
export class StatesService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
  }

  private readonly _reload$: Subject<void> = new Subject<void>();

  public search(dto: IPaginationRequest): Observable<IPagination<IState>> {
    return concat(of(0), this._reload$).pipe(
      switchMap(() => this._httpClient.post<IPagination<IState>>('/api/State/search', dto))
    );
  }

  public assign(dto: IStateAssignRequest): Observable<boolean> {
    return this._httpClient.post<boolean>('/api/State/assign', dto).pipe(
      tap(() => this._reload$.next())
    );
  }

  public create(dto: IStateCreateRequest): Observable<boolean> {
    return this._httpClient.post<boolean>('/api/State/createWithDto', dto).pipe(
      tap(() => this._reload$.next())
    )
  }
}
