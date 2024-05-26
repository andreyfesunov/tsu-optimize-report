import {Injectable} from "@angular/core";
import {IPaginationRequest} from "@core/dtos";
import {concat, Observable, of, Subject, switchMap, tap} from "rxjs";
import {IPagination, IReport} from "@core/models";
import {HttpClient} from "@angular/common/http";

@Injectable({providedIn: "root"})
export class ReportsService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
  }

  private readonly _reload$: Subject<void> = new Subject<void>();

  public search(dto: IPaginationRequest): Observable<IPagination<IReport>> {
    return concat(of(0), this._reload$).pipe(
      switchMap(() => this._httpClient.post<IPagination<IReport>>('/api/Report/search', dto))
    );
  }

  public create(id: string, data: FormData): Observable<boolean> {
    return this._httpClient.post<boolean>(`/api/Report/${id}/CreateReport`, data).pipe(
      tap(() => this._reload$.next())
    );
  }
}
