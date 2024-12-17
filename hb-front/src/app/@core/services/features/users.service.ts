import {Injectable, inject} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {IPaginationRequest} from "@core/dtos";
import {Observable, shareReplay} from "rxjs";
import {IPagination, IUser} from "@core/models";
import { environment } from "src/environments/environment";
@Injectable({providedIn: "root"})
export class UsersService {
  private readonly _apiRoot = environment.apiRoot;
  private readonly _httpClient = inject(HttpClient);

  private readonly _users$ = this._httpClient.get<IUser[]>(`${this._apiRoot}/User`).pipe(
    shareReplay(1)
  );

  public getAll(): Observable<IUser[]> {
    return this._users$;
  }

  public search(dto: IPaginationRequest): Observable<IPagination<IUser>> {
    return this._httpClient.post<IPagination<IUser>>(`${this._apiRoot}/User/search`, dto);
  }
}
