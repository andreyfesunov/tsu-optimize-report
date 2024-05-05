import {IToken} from "@shared/models";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {AuthService} from "@shared/services";

@Injectable()
export class AuthServiceImpl extends AuthService {
  constructor(
    private readonly _http: HttpClient
  ) {
    super();
  }

  public logIn(credentials: { email: string; password: string }): Observable<IToken> {
    return this._http.post<IToken>("/api/User/log-in", credentials);
  }

  public reg(credentials: { email: string; password: string }): Observable<boolean> {
    return this._http.post<boolean>("/api/User/reg", credentials);
  }
}
