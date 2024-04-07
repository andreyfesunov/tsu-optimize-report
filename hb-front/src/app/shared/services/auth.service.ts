import {Observable} from "rxjs";
import {IToken} from "../interfaces";

export abstract class AuthService {
  public abstract logIn(credentials: { login: string, password: string }): Observable<IToken>
}