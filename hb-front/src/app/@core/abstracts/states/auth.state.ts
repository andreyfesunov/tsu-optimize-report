import {map, Observable, shareReplay} from "rxjs";
import {exists, fromLocalStorage, parseJwt} from "@core/utils";

export abstract class AuthState<TEntity> {
    public readonly tokenRaw$: Observable<string | undefined> = fromLocalStorage(this._tokenKey).pipe(
        shareReplay({
            bufferSize: 1,
            refCount: true
        })
    );
    public readonly token$: Observable<string> = this.tokenRaw$.pipe(
        exists(),
        shareReplay({
            bufferSize: 1,
            refCount: true
        })
    );
    public readonly user$: Observable<TEntity> = this.token$.pipe(
        map((token) => parseJwt(token)),
        shareReplay({
            bufferSize: 1,
            refCount: true
        })
    );

    protected constructor(private readonly _tokenKey: string) {
    }

    public isTokenValid(): boolean {
        const token = localStorage.getItem(this._tokenKey);
        if (token) {
            const base64Url = token.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const payload = JSON.parse(atob(base64));
            const exp = payload.exp;
            console.log(payload);
            if (exp) {
                const currentTime = Math.floor(Date.now() / 1000);
                return currentTime < exp;
            }
        }
        return false;
    }

    public setToken(accessToken: string): void {
        localStorage.setItem(this._tokenKey, JSON.stringify(accessToken));
    }

    public removeToken(): void {
        localStorage.removeItem(this._tokenKey);
    }
}
