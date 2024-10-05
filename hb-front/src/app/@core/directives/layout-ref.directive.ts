import {Directive, inject, input, TemplateRef} from "@angular/core";

export enum LayoutRefs {
  ACTIONS = 'ACTIONS'
}

@Directive({
  selector: '[appLayoutRef]',
  standalone: true
})
export class LayoutRefDirective {
  public readonly templateRef = inject(TemplateRef<unknown>);
  public readonly appLayoutRef = input.required<string>();
}
