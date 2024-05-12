import {Component} from "@angular/core";
import {ScrollableComponent} from "@ui/widgets";

@Component({
  selector: "app-content",
  standalone: true,
  template: `
    <div class="content content--padding">
      <div class="content__inner host-class">
        <app-scrollable>
          <ng-content></ng-content>
        </app-scrollable>
      </div>
    </div>
  `,
  styleUrls: ["content.component.scss"],
  imports: [
    ScrollableComponent
  ],
  host: {class: 'host-class'}
})
export class ContentComponent {

}
