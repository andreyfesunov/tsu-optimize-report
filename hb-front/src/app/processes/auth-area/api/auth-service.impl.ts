import {AuthService, IToken} from "../../../shared";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export class AuthServiceImpl extends AuthService {
  constructor(
    private readonly _http: HttpClient
  ) {
    super();
  }

  public logIn(credentials: { login: string; password: string }): Observable<IToken> {
    return this._http.post<IToken>('api/User/login', credentials);
  }

  public reg(credentials: { login: string; password: string; confirmPassword: string }): Observable<boolean> {
    return this._http.post<boolean>('api/User/registrate', credentials);
  }
}
