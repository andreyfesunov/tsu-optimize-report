import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IEventType, IPagination} from "@core/models";

export abstract class EventTypesService {
  public abstract search(activityId: string, req: IPaginationRequest): Observable<IPagination<IEventType>>;
}
