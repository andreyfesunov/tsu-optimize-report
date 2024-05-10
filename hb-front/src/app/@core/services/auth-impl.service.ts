import {IToken} from "@core/models";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export class AuthServiceImpl {
    constructor(
        private readonly _http: HttpClient
    ) {
    }

    public logIn(credentials: { email: string; password: string }): Observable<IToken> {
        return this._http.post<IToken>("/api/User/log-in", credentials);
    }

    public reg(credentials: { email: string; password: string }): Observable<string> {
        return this._http.post<string>("/api/User/reg", credentials);
    }
}
