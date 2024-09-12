import {Directive, input, TemplateRef} from "@angular/core";

export enum LayoutRefs {
  ACTIONS = 'ACTIONS'
}

@Directive({
  selector: '[appLayoutRef]',
  standalone: true
})
export class LayoutRefDirective {
  public readonly appLayoutRef = input.required<string>();

  constructor(public readonly templateRef: TemplateRef<unknown>) {
  }
}
