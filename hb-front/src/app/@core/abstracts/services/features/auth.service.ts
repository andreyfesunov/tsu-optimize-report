import {Observable} from "rxjs";
import {ILoginRegDto, ITokenDto} from "@core/dtos";

export abstract class AuthService {
    public abstract logIn(credentials: ILoginRegDto): Observable<ITokenDto>;

    public abstract reg(credentials: ILoginRegDto): Observable<string>;
}
