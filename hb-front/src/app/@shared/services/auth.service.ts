import {Observable} from "rxjs";
import {IToken} from "@shared/models";

export abstract class AuthService {
  public abstract logIn(credentials: { email: string, password: string }): Observable<IToken>;

  public abstract reg(credentials: { email: string; password: string }): Observable<string>;
}
