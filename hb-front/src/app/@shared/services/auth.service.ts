import {Observable} from "rxjs";
import {IToken} from "@shared/models";

export abstract class AuthService {
  public abstract logIn(credentials: { login: string, password: string }): Observable<IToken>;

  public abstract reg(credentials: { login: string; password: string }): Observable<boolean>;
}
