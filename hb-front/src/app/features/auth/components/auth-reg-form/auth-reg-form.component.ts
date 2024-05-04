import { Component, EventEmitter, Output } from "@angular/core";
import { MatInputModule } from "@angular/material/input";
import { FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { RouterService } from "@core/services";

@Component({
  selector: 'app-auth-reg-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './auth-reg-form.component.html',
  styleUrls: ['../../auth-form-base.scss']
})
export class AuthRegFormComponent {
  @Output() public readonly submitEvent: EventEmitter<{ login: string; password: string }> = new EventEmitter<{
    login: string;
    password: string
  }>()

  protected readonly form: FormGroup<IAuthRegForm> = this._buildForm();

  public constructor(
    private readonly _fb: NonNullableFormBuilder,
    private readonly _routerService: RouterService,
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

  protected toLogin(): void {
    this._routerService.navigate(['auth/login']);
  }

  private _buildForm(): FormGroup<IAuthRegForm> {
    return this._fb.group<IAuthRegForm>({
      login: this._fb.control<string>('', Validators.required),
      password: this._fb.control<string>('', Validators.required)
    })
  }
}

export interface IAuthRegForm {
  login: FormControl<string>;
  password: FormControl<string>;
}
