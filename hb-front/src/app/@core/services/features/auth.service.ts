import {ILoginRegDto, ITokenDto} from "@core/dtos";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable({providedIn: "root"})
export class AuthService {
  constructor(
    private readonly _http: HttpClient
  ) {
  }

  public logIn(credentials: ILoginRegDto): Observable<ITokenDto> {
    return this._http.post<ITokenDto>("/api/User/log-in", credentials);
  }

  public reg(credentials: ILoginRegDto): Observable<string> {
    return this._http.post<string>("/api/User/reg", credentials);
  }
}
