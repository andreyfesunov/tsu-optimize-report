import {Directive, HostListener, Input, output} from "@angular/core";
import {ITableColumn} from "@core/models";

@Directive()
export class TableRowController<TEntity, TCol extends string> {
  @Input({required: true}) item: TEntity | null = null;
  @Input({required: true}) cols: ITableColumn<TCol>[] = [];
  @HostListener('mouseenter') onMouseEnter = () => this.hovered = true;
  @HostListener('mouseleave') onMouseLeave = () => this.hovered = false;
  @HostListener('click') onMouseClick = () => this.onclick.emit();

  protected hovered: boolean = false;
  protected readonly onclick = output<void>();
}
