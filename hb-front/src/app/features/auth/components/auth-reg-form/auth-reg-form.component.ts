import {Component, EventEmitter, Output} from "@angular/core";
import {MatInputModule} from "@angular/material/input";
import {FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";

@Component({
  selector: "app-auth-reg-form",
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: "./auth-reg-form.component.html",
  styleUrls: ["../auth-form-base.scss"]
})
export class AuthRegFormComponent {
  public constructor(
    private readonly _fb: NonNullableFormBuilder,
  ) {
  }

  @Output() public readonly submitEvent: EventEmitter<{ email: string; password: string }> = new EventEmitter<{
    email: string;
    password: string
  }>()

  @Output() public readonly redirectEvent: EventEmitter<void> = new EventEmitter<void>();

  protected readonly form: FormGroup<IAuthRegForm> = this._buildForm();

  private _buildForm(): FormGroup<IAuthRegForm> {
    return this._fb.group<IAuthRegForm>({
      email: this._fb.control<string>("", [Validators.required, Validators.email]),
      password: this._fb.control<string>("", Validators.required)
    })
  }

  protected toLogin(): void {
    this.redirectEvent.next();
  }

  protected submit(): void {
    if (this.form.invalid) {
      return this.form.markAllAsTouched();
    }

    const request = {
      email: this.form.controls.email.value,
      password: this.form.controls.password.value
    };

    this.submitEvent.next(request);
  }
}

export interface IAuthRegForm {
  email: FormControl<string>;
  password: FormControl<string>;
}
