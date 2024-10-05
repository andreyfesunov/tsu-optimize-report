import {ILoginDto, ITokenDto} from "@core/dtos";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable, inject} from "@angular/core";

@Injectable({providedIn: "root"})
export class AuthService {
  private readonly _http = inject(HttpClient);

  public logIn(credentials: ILoginDto): Observable<ITokenDto> {
    return this._http.post<ITokenDto>("/api/User/log-in", credentials);
  }

  public reg(credentials: ILoginDto): Observable<string> {
    return this._http.post<string>("/api/User/reg", credentials);
  }
}
