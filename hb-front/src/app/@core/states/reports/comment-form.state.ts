import {IComment} from "@core/models";
import {EventsService} from "@core/services";
import {DestroyRef} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";

export class CommentFormState {
  constructor(
    private readonly _eventId: string,
    private readonly _comment: IComment | null,
    private readonly _eventsService: EventsService,
    private readonly _destroyRef: DestroyRef
  ) {
    // this._init();
  }

  public readonly form: FormGroup<ICommentForm> = new FormGroup<ICommentForm>({
    content: new FormControl<string>('', {nonNullable: true}),
    fact: new FormControl<number | null>(null, {nonNullable: true}),
    plan: new FormControl<number | null>(null, {nonNullable: true}),
  })
}

export interface ICommentForm {
  content: FormControl<string>;
  plan: FormControl<number | null>;
  fact: FormControl<number | null>;
}
