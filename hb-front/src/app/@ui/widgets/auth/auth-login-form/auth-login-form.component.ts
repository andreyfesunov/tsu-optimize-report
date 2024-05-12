import {Component, EventEmitter, Output} from "@angular/core";
import {MatInputModule} from "@angular/material/input";
import {FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";
import {ILoginRegDto} from "@core/dtos";

@Component({
  selector: "app-auth-login-form",
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: "auth-login-form.component.html",
  styleUrls: ["../auth-form-base.scss"],
  host: {class: 'host-class'}

})
export class AuthLoginFormComponent {
  public constructor(
    private readonly _fb: NonNullableFormBuilder
  ) {
  }

  @Output() public readonly submitEvent: EventEmitter<ILoginRegDto> = new EventEmitter<ILoginRegDto>()

  @Output() public readonly redirectEvent: EventEmitter<void> = new EventEmitter<void>();

  protected readonly form: FormGroup<IAuthLoginForm> = this._buildForm();

  protected submit(): void {
    if (this.form.invalid) {
      return this.form.markAllAsTouched();
    }

    const request: ILoginRegDto = {
      email: this.form.controls.email.value,
      password: this.form.controls.password.value
    };

    this.submitEvent.next(request);
  }

  protected toRegister(): void {
    this.redirectEvent.next();
  }

  private _buildForm(): FormGroup<IAuthLoginForm> {
    return this._fb.group<IAuthLoginForm>({
      email: this._fb.control<string>("", [Validators.required, Validators.email]),
      password: this._fb.control<string>("", Validators.required)
    })
  }
}

export interface IAuthLoginForm {
  email: FormControl<string>;
  password: FormControl<string>;
}
