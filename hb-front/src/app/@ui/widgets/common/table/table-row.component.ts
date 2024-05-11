import {Input} from "@angular/core";

export class TableRowComponent<TEntity> {
    @Input({required: true}) item: TEntity | null = null;
}
