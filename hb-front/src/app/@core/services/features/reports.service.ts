import {inject, Injectable} from "@angular/core";
import {IPaginationRequest} from "@core/dtos";
import {concat, Observable, of, Subject, switchMap, tap} from "rxjs";
import {IEventType, IPagination, IReportDetail, IReportListItem} from "@core/models";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import { environment } from "src/environments/environment";

@Injectable({providedIn: "root"})
export class ReportsService {
  private readonly _apiRoot = environment.apiRoot;
  private readonly _httpClient = inject(HttpClient);
  private readonly _reload$: Subject<void> = new Subject<void>();

  public search(dto: IPaginationRequest): Observable<IPagination<IReportListItem>> {
    return concat(of(0), this._reload$).pipe(
      switchMap(() => this._httpClient.post<IPagination<IReportListItem>>(`${this._apiRoot}/Report/search`, dto))
    );
  }

  public create(id: string, data: FormData): Observable<boolean> {
    return this._httpClient.post<boolean>(`${this._apiRoot}/Report/${id}/CreateReport`, data).pipe(
      tap(() => this._reload$.next())
    );
  }

  public detail(id: string): Observable<IReportDetail> {
    return this._httpClient.get<IReportDetail>(`${this._apiRoot}/Report/${id}`);
  }

  public export(id: string): Observable<Blob> {
    return this._httpClient.get<Blob>(`${this._apiRoot}/Report/${id}/ExportReport`, {
      headers: new HttpHeaders().append('Content-Type', 'application/json'),
      responseType: 'blob' as 'json'
    });
  }

  public eventTypes(reportId: string): Observable<readonly IEventType[]> {
    return this._httpClient.get<readonly IEventType[]>(`${this._apiRoot}/Report/${reportId}/event-type`);
  }
}
