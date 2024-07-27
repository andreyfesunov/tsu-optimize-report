import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {IPaginationRequest} from "@core/dtos";
import {Observable, shareReplay} from "rxjs";
import {IPagination, IUser} from "@core/models";

@Injectable({providedIn: "root"})
export class UsersService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
  }

  private readonly _users$ = this._httpClient.get<IUser[]>('/api/User').pipe(
    shareReplay(1)
  );

  public getAll(): Observable<IUser[]> {
    return this._users$;
  }

  public search(dto: IPaginationRequest): Observable<IPagination<IUser>> {
    return this._httpClient.post<IPagination<IUser>>('/api/User/search', dto);
  }
}
