import {UsersService} from "@core/abstracts";
import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IPagination, IUser} from "@core/models";

@Injectable()
export class UsersImplService extends UsersService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
    super();
  }

  public search(dto: IPaginationRequest): Observable<IPagination<IUser>> {
    return this._httpClient.post<IPagination<IUser>>('/api/User/search', dto);
  }
}
