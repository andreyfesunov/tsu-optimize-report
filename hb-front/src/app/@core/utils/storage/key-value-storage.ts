export class KeyValueStorage {
    public constructor(private readonly _storage: Storage) {
    }

    public getItem(key: string): string | undefined {
        return this._storage.getItem(key) || undefined;
    }

    public setItem(key: string, value: string | undefined = undefined): void {
        const oldValue = this.getItem(key);

        if (oldValue !== value) {
            if (value !== undefined) {
                localStorage.setItem(key, value);
            } else {
                localStorage.removeItem(key);
            }

            window.dispatchEvent(
                new StorageEvent('storage', {
                    key,
                    newValue: value,
                    oldValue: oldValue
                })
            );
        }
    }
}
