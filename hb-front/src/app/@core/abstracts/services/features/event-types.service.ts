import {IActivitiesAssignEventRequest, IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IEventType, IPagination} from "@core/models";

export abstract class EventTypesService {
  public abstract getAll(): Observable<IEventType[]>;

  public abstract getAllForReport(reportId: string, workId: string): Observable<IEventType[]>;

  public abstract assign(req: IActivitiesAssignEventRequest): Observable<boolean>;

  public abstract search(activityId: string, req: IPaginationRequest): Observable<IPagination<IEventType>>;
}
