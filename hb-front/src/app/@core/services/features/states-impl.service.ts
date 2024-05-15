import {StatesService} from "@core/abstracts";
import {IPaginationRequest, IStateCreateRequest} from "@core/dtos";
import {Observable} from "rxjs";
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

  public search(dto: IPaginationRequest): Observable<IPagination<IState>> {
    return this._httpClient.post<IPagination<IState>>('/api/State/search', dto);
  }

  public create(dto: IStateCreateRequest): Observable<void> {
    return this._httpClient.post<void>('/api/State/create', dto);
  }
}
