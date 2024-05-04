import {Component} from "@angular/core";
import { AuthRegFormComponent } from "@features/auth/components/auth-reg-form/auth-reg-form.component";

@Component({
  standalone: true,
  imports: [
    AuthRegFormComponent,
  ],
  template: `
    <app-auth-reg-form (submitEvent)="onSubmit()"></app-auth-reg-form>
  `
})
export class AuthRegComponent {

  protected onSubmit(): void {
  }
}
