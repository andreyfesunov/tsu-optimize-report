import {ILoginDto, ITokenDto} from "@core/dtos";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable, inject} from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({providedIn: "root"})
export class AuthService {
  private readonly _apiRoot = environment.apiRoot;
  private readonly _http = inject(HttpClient);

  public logIn(credentials: ILoginDto): Observable<ITokenDto> {
    return this._http.post<ITokenDto>(`${this._apiRoot}/User/log-in`, credentials);
  }

  public reg(credentials: ILoginDto): Observable<string> {
    return this._http.post<string>(`${this._apiRoot}/User/reg`, credentials);
  }
}
