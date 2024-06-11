import {
  IAutocompleteOptionSource,
  IAutocompleteOptionSourceQueryParams,
  ISelectOption,
  ISelectOptionAutocomplete
} from "@core/models";
import {combineLatest, map, Observable} from "rxjs";
import {excludeOptions} from "@core/utils";

export class ExcludeAutocompleteOptionSource<TPayload> implements IAutocompleteOptionSource<TPayload> {
  constructor(
    private readonly _inner: IAutocompleteOptionSource<TPayload>,
    private readonly _exclude$: Observable<readonly string[]>
  ) {
  }

  public searchByValue(v: string): Observable<ISelectOption<TPayload>[]> {
    return this._inner.searchByValue(v);
  }

  public search(query: string, params: IAutocompleteOptionSourceQueryParams): Observable<ISelectOptionAutocomplete<TPayload>[]> {
    return combineLatest([this._inner.search(query, params), this._exclude$]).pipe(
      map(([root, exclude]) => excludeOptions(root, exclude))
    )
  }
}
