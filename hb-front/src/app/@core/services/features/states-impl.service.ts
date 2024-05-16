import {StatesService} from "@core/abstracts";
import {IPaginationRequest, IStateCreateRequest} from "@core/dtos";
import {concat, Observable, of, Subject, switchMap, tap} from "rxjs";
import {IPagination, IState} from "@core/models";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export class StatesImplService extends StatesService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
    super();
  }

  private readonly _reload$: Subject<void> = new Subject<void>();

  public search(dto: IPaginationRequest): Observable<IPagination<IState>> {
    return concat(of(0), this._reload$).pipe(
      switchMap(() => this._httpClient.post<IPagination<IState>>('/api/State/search', dto))
    );
  }

  public create(dto: IStateCreateRequest): Observable<boolean> {
    return this._httpClient.post<boolean>('/api/State/create', dto).pipe(
      tap(() => this._reload$.next())
    )
  }
}
