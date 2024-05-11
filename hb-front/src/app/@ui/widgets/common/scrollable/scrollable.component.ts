import {Component, Input, ViewEncapsulation} from "@angular/core";

@Component({
    selector: 'app-scrollable',
    standalone: true,
    template: `
        <ng-content></ng-content>`,
    styleUrls: ['scrollable.component.scss'],
    encapsulation: ViewEncapsulation.None,
    host: {
        class: 'scrollable',
        '[class.scrollable--h-hidden]': '!horizontalScroll',
        '[class.scrollable--v-hidden]': '!verticalScroll',
    }
})
export class ScrollableComponent {
    @Input() horizontalScroll: boolean = false;
    @Input() verticalScroll: boolean = true;
}
