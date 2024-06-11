import {ISelectOptionBase} from "@core/models";

export function excludeOptions<TOption extends ISelectOptionBase<TPayload>, TPayload>(
  options: TOption[],
  values: readonly string[]
): TOption[] {
  return options.filter((opt) => !values.includes(opt.value));
}
