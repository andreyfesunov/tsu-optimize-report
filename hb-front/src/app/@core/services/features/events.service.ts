import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {IEventCreateDto, IEventUpdateDto} from "@core/dtos";
import {Observable} from "rxjs";
import {IEvent} from "@core/models";

@Injectable({providedIn: "root"})
export class EventsService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
  }

  public create(dto: IEventCreateDto): Observable<IEvent> {
    return this._httpClient.post<IEvent>('/api/Event/create', dto);
  }

  public update(dto: IEventUpdateDto): Observable<IEvent> {
    return this._httpClient.post<IEvent>('/api/Event/update', dto);
  }
}
