import {Observable} from "rxjs";
import {IAutocompleteOptionSourceQueryParams, ISelectOption, ISelectOptionAutocomplete} from "@core/models";

export interface IAutocompleteOptionSource<TPayload> {
  searchByValue(v: string): Observable<ISelectOption<TPayload>[]>

  search(query: string, params: IAutocompleteOptionSourceQueryParams): Observable<ISelectOptionAutocomplete<TPayload>[]>;
}
