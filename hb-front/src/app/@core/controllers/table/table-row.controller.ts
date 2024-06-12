import {Directive, HostBinding, HostListener, Input} from "@angular/core";
import {ITableColumn} from "@core/models";

@Directive()
export class TableRowController<TEntity, TCol extends string> {
  @Input({required: true}) item: TEntity | null = null;
  @Input({required: true}) cols: ITableColumn<TCol>[] = [];
  @Input() clickable: boolean = true;

  @HostListener('mouseenter') onMouseEnter = () => this.clickable && (this.hovered = true);
  @HostListener('mouseleave') onMouseLeave = () => this.clickable && (this.hovered = false);

  @HostBinding('style.cursor') get cursor(): string {
    return this.clickable ? 'pointer' : 'default';
  }

  protected hovered: boolean = false;
}
