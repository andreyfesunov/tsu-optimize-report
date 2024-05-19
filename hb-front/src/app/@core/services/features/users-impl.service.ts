import {UsersService} from "@core/abstracts";
import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {IPaginationRequest} from "@core/dtos";
import {Observable, shareReplay} from "rxjs";
import {IPagination, IUser} from "@core/models";

@Injectable()
export class UsersImplService extends UsersService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
    super();
  }

  private readonly _users$ = this._httpClient.get<IUser[]>('/api/User/getAll').pipe(
    shareReplay(1)
  );

  public getAll(): Observable<IUser[]> {
    return this._users$;
  }

  public search(dto: IPaginationRequest): Observable<IPagination<IUser>> {
    return this._httpClient.post<IPagination<IUser>>('/api/User/search', dto);
  }
}
