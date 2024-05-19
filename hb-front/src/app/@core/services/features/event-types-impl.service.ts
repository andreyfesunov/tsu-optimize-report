import {EventTypesService} from "@core/abstracts";
import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IEventType, IPagination} from "@core/models";

@Injectable()
export class EventTypesImplService extends EventTypesService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
    super();
  }

  public search(activityId: string, req: IPaginationRequest): Observable<IPagination<IEventType>> {
    return this._httpClient.post<IPagination<IEventType>>(`/api/EventType/${activityId}/search`, req);
  }
}
