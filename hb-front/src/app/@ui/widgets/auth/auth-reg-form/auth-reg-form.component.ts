import {Component, inject, output} from "@angular/core";
import {MatInputModule} from "@angular/material/input";
import {FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {MatFormFieldModule} from "@angular/material/form-field";
import {ILoginDto, IRegDto} from "@core/dtos";

@Component({
  selector: "app-auth-reg-form",
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: "auth-reg-form.component.html",
  styleUrls: ["../auth-form-base.scss"],
  host: {class: 'host-class'}

})
export class AuthRegFormComponent {
  private readonly _fb = inject(NonNullableFormBuilder);

  public readonly submitEvent = output<ILoginDto>()
  public readonly redirectEvent = output<void>();

  protected readonly form: FormGroup<IAuthRegForm> = this._buildForm();

  protected toLogin(): void {
    this.redirectEvent.emit();
  }

  protected submit(): void {
    if (this.form.invalid) {
      return this.form.markAllAsTouched();
    }

    const request: IRegDto = {
      email: this.form.controls.email.value,
      firstname: this.form.controls.firstname.value,
      lastname: this.form.controls.lastname.value,
      password: this.form.controls.password.value
    };

    this.submitEvent.emit(request);
  }

  private _buildForm(): FormGroup<IAuthRegForm> {
    return this._fb.group<IAuthRegForm>({
      email: this._fb.control<string>("", [Validators.required, Validators.email]),
      firstname: this._fb.control<string>("", [Validators.required]),
      lastname: this._fb.control<string>("", [Validators.required]),
      password: this._fb.control<string>("", Validators.required)
    })
  }
}

export interface IAuthRegForm {
  email: FormControl<string>;
  firstname: FormControl<string>;
  lastname: FormControl<string>;
  password: FormControl<string>;
}
