import {Component, input} from "@angular/core";
import {MatProgressSpinner} from "@angular/material/progress-spinner";

@Component({
  selector: 'app-spinner',
  standalone: true,
  imports: [
    MatProgressSpinner
  ],
  template: `
    <mat-spinner [diameter]="diameter()"></mat-spinner>
  `,
  styles: [
    `
      :host {
        position: absolute;
        top: 0;
        left: 0;

        width: 100%;
        height: 100%;

        display: grid;
        place-items: center;
      }
    `
  ]
})
export class SpinnerComponent {
  public readonly diameter = input<number>(100);
}
