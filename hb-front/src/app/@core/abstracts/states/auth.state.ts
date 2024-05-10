import {map, Observable, shareReplay} from "rxjs";
import {exists, fromLocalStorage, isTokenValid, LocalStorage, parseJwt} from "@core/utils";

export abstract class AuthState<TEntity> {
    protected constructor(private readonly _tokenKey: string) {
    }

    public readonly tokenRaw$: Observable<string | undefined> = fromLocalStorage(this._tokenKey).pipe(
        shareReplay({
            bufferSize: 1,
            refCount: false
        })
    );
    public readonly valid$: Observable<boolean> = this.tokenRaw$.pipe(
        map((token) => isTokenValid(token))
    )
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

    public setToken(accessToken: string): void {
        LocalStorage.setItem(this._tokenKey, accessToken);
    }

    public removeToken(): void {
        LocalStorage.setItem(this._tokenKey);
    }
}
