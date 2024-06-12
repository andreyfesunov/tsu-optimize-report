import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {IActivitiesAssignEventRequest, IPaginationRequest} from "@core/dtos";
import {concat, filter, map, Observable, of, shareReplay, Subject, switchMap, take, tap} from "rxjs";
import {IEventType, IPagination} from "@core/models";

@Injectable({providedIn: "root"})
export class EventTypesService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
  }

  private readonly _activityId$: Subject<string> = new Subject<string>();

  private readonly _eventTypes$: Observable<IEventType[]> = this._httpClient.get<IEventType[]>('/api/EventType/getAll').pipe(
    shareReplay(1)
  )

  private _eventTypesMap$: Observable<{ [key: string]: IPagination<IEventType> }> | null = null;

  public getAll(): Observable<IEventType[]> {
    return this._eventTypes$;
  }

  public getAllForReport(reportId: string, workId: string): Observable<IEventType[]> {
    return this._httpClient.get<IEventType[]>(`/api/EventType/getAll/${reportId}/${workId}`);
  }

  public search(activityId: string, req: IPaginationRequest): Observable<IPagination<IEventType>> {
    return concat(of(0), this.activityEvent$(activityId)).pipe(
      switchMap(() => this._getSearchRequest(activityId, req))
    );
  }

  public assign(req: IActivitiesAssignEventRequest): Observable<boolean> {
    return this._httpClient.post<boolean>('/api/EventType/assign', req).pipe(
      tap(() => this._activityId$.next(req.activityId))
    );
  }

  private readonly activityEvent$ = (id: string) => this._activityId$.pipe(
    filter((activityId) => activityId === id)
  );

  private _getSearchRequest(activityId: string, req: IPaginationRequest): Observable<IPagination<IEventType>> {
    const defaultRequest = this._httpClient.post<IPagination<IEventType>>(`/api/EventType/${activityId}/search`, req);

    if (req.pageNumber !== 1) {
      return defaultRequest;
    }

    return (this._eventTypesMap$ ?? (this._eventTypesMap$ = concat(of(0), this._activityId$).pipe(
      switchMap(() => this._httpClient.post<{
        [key: string]: IPagination<IEventType>
      }>(`/api/EventType/searchMap`, req)),
      shareReplay({bufferSize: 1, refCount: true})
    ))).pipe(
      map((map) => map[activityId]),
      take(1)
    );
  }
}
