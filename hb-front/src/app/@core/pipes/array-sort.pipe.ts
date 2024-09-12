import {Pipe, PipeTransform} from "@angular/core";
import {ArrayUtils} from "@core/utils";

@Pipe({
  name: 'sort',
  standalone: true
})
export class ArraySortPipe implements PipeTransform {
  transform<T, TKey extends keyof T>(array: T[], field: TKey): T[] {
    if (!Array.isArray(array)) {
      return array;
    }
    return ArrayUtils.sortByField(array, field);
  }
}
