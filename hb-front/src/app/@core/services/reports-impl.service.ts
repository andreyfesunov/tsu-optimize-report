import {ReportsService} from "@core/abstracts";
import {Injectable} from "@angular/core";
import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IPagination, IReport} from "@core/models";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class ReportsImplService extends ReportsService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
    super();
  }

  public search(dto: IPaginationRequest): Observable<IPagination<IReport>> {
    return this._httpClient.post<IPagination<IReport>>('/api/Report/search', dto);
  }
}
