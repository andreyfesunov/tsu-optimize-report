import {Directive, HostListener, Input} from "@angular/core";
import {ITableColumn} from "@core/models";

@Directive()
export class TableRowController<TEntity, TCol extends string> {
    @Input({required: true}) item: TEntity | null = null;
    @Input({required: true}) cols: ITableColumn<TCol>[] = [];
    @HostListener('mouseenter') onMouseEnter = () => this.hovered = true;
    @HostListener('mouseleave') onMouseLeave = () => this.hovered = false;

    protected hovered: boolean = false;
}
