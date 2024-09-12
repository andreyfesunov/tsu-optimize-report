import {inject, Injectable} from "@angular/core";
import {IPaginationRequest} from "@core/dtos";
import {concat, Observable, of, Subject, switchMap, tap} from "rxjs";
import {IEventType, IPagination, IReportDetail, IReportListItem} from "@core/models";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Injectable({providedIn: "root"})
export class ReportsService {
  private readonly _httpClient = inject(HttpClient);
  private readonly _reload$: Subject<void> = new Subject<void>();

  public search(dto: IPaginationRequest): Observable<IPagination<IReportListItem>> {
    return concat(of(0), this._reload$).pipe(
      switchMap(() => this._httpClient.post<IPagination<IReportListItem>>('/api/Report/search', dto))
    );
  }

  public create(id: string, data: FormData): Observable<boolean> {
    return this._httpClient.post<boolean>(`/api/Report/${id}/CreateReport`, data).pipe(
      tap(() => this._reload$.next())
    );
  }

  public detail(id: string): Observable<IReportDetail> {
    return this._httpClient.get<IReportDetail>(`/api/Report/${id}`);
  }

  public export(id: string): Observable<Blob> {
    return this._httpClient.get<Blob>(`/api/Report/${id}/ExportReport`, {
      headers: new HttpHeaders().append('Content-Type', 'application/json'),
      responseType: 'blob' as 'json'
    });
  }

  public eventTypes(reportId: string): Observable<readonly IEventType[]> {
    return this._httpClient.get<readonly IEventType[]>(`/api/Report/${reportId}/event-type`);
  }
}
