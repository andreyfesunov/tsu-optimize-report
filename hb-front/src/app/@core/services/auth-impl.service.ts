import {ILoginRegDto, ITokenDto} from "@core/dtos";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {AuthService} from "@core/abstracts";

@Injectable()
export class AuthServiceImpl extends AuthService {
    constructor(
        private readonly _http: HttpClient
    ) {
        super();
    }

    public logIn(credentials: ILoginRegDto): Observable<ITokenDto> {
        return this._http.post<ITokenDto>("/api/User/log-in", credentials);
    }

    public reg(credentials: ILoginRegDto): Observable<string> {
        return this._http.post<string>("/api/User/reg", credentials);
    }
}
