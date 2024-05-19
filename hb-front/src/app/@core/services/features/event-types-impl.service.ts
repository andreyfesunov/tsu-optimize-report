import {EventTypesService} from "@core/abstracts";
import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {IActivitiesAssignEventRequest, IPaginationRequest} from "@core/dtos";
import {concat, filter, Observable, of, shareReplay, Subject, switchMap, tap} from "rxjs";
import {IEventType, IPagination} from "@core/models";

@Injectable()
export class EventTypesImplService extends EventTypesService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
    super();
  }

  private readonly _activityId$: Subject<string> = new Subject<string>();

  private readonly _eventTypes$: Observable<IEventType[]> = this._httpClient.get<IEventType[]>('/api/EventType/getAll').pipe(
    shareReplay(1)
  )

  public getAll(): Observable<IEventType[]> {
    return this._eventTypes$;
  }

  public search(activityId: string, req: IPaginationRequest): Observable<IPagination<IEventType>> {
    return concat(of(0), this.activityEvent$(activityId)).pipe(
      switchMap(() => this._httpClient.post<IPagination<IEventType>>(`/api/EventType/${activityId}/search`, req))
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
}
