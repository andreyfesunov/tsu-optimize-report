import {Observable} from "rxjs";
import {KeyValueStorage} from "./key-value-storage";

export const LocalStorage = new KeyValueStorage(localStorage);

export function fromLocalStorage<T extends string>(key: string): Observable<T | undefined> {
    return new Observable<T | undefined>((subscriber) => {
        subscriber.next(LocalStorage.getItem(key) as T | undefined);

        function handler(evt: StorageEvent) {
            if (evt.key === key && evt.oldValue !== evt.newValue) {
                subscriber.next(evt.newValue as T | undefined);
            }
        }

        window.addEventListener('storage', handler);

        return () => {
            window.removeEventListener('storage', handler);
        };
    });
}
