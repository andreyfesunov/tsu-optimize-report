export interface ISelectOptionBase<TPayload> {
  value: string;
  payload: TPayload;
}

export interface ISelectOption<TPayload> extends ISelectOptionBase<TPayload> {
  label: string;
}

export interface ISelectOptionAutocomplete<TPayload> extends ISelectOptionBase<TPayload> {
  label: {
    text: string;
    regions: readonly ITextSearchMatchRegion[];
  }
}

export interface ITextSearchMatchRegion {
  /**
   * Inclusive start
   */
  readonly start: number;
  /**
   * Inclusive end
   */
  readonly end: number;
}

export interface IAutocompleteOptionSourceQueryParams {
  readonly excludeIds?: readonly string[];
}
