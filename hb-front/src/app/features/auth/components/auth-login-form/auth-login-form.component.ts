import {Component, EventEmitter, Output} from "@angular/core";
import {MatInput} from "@angular/material/input";
import {FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatButton} from "@angular/material/button";

@Component({
  selector: 'app-auth-login-form',
  standalone: true,
  imports: [
    MatInput,
    ReactiveFormsModule,
    MatButton
  ],
  template: `
    <div class="auth-login-form">
      <input matInput [formControl]="form.controls.login">
      <input matInput type="password" [formControl]="form.controls.password">

      <button mat-button (click)="submit()">Войти</button>
    </div>
  `,
  styleUrls: ['auth-login-form.component.scss']
})
export class AuthLoginFormComponent {
  @Output() public readonly submitEvent: EventEmitter<{ login: string; password: string }> = new EventEmitter<{
    login: string;
    password: string
  }>()

  protected readonly form: FormGroup<IAuthLoginForm> = this._buildForm();

  public constructor(
    private readonly _fb: NonNullableFormBuilder
  ) {
  }

  protected submit(): void {
    if (this.form.invalid) {
      return this.form.markAllAsTouched();
    }

    const request = {
      login: this.form.controls.login.value,
      password: this.form.controls.login.value
    };

    this.submitEvent.next(request);
  }

  private _buildForm(): FormGroup<IAuthLoginForm> {
    return this._fb.group<IAuthLoginForm>({
      login: this._fb.control<string>('', Validators.required),
      password: this._fb.control<string>('', Validators.required)
    })
  }
}

export interface IAuthLoginForm {
  login: FormControl<string>;
  password: FormControl<string>;
}
