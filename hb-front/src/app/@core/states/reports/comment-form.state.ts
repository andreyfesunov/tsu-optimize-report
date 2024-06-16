import {IComment} from "@core/models";
import {EventsService} from "@core/services";
import {DestroyRef} from "@angular/core";
import {FormControl, FormGroup} from "@angular/forms";
import {BehaviorSubject, map, merge, Observable, Subscription, switchMap, tap, throttleTime} from "rxjs";
import {ICommentCreateDto, ICommentUpdateDto} from "@core/dtos";
import {exists, required} from "@core/utils";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";

export class CommentFormState {
  constructor(
    private readonly _eventId: string,
    public readonly comment: IComment | null,
    private readonly _eventsService: EventsService,
    private readonly _destroyRef: DestroyRef
  ) {
    this._init();
  }

  private readonly _id$: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

  private readonly _commentSubscription: Subscription = new Subscription();

  public readonly form: FormGroup<ICommentForm> = new FormGroup<ICommentForm>({
    content: new FormControl<string>('', {nonNullable: true}),
    fact: new FormControl<number | null>(null, {nonNullable: true}),
    plan: new FormControl<number | null>(null, {nonNullable: true}),
  })

  private _init(): void {
    this.comment && this._bindForm(this.comment);
    this.comment && this._id$.next(this.comment.id);

    merge(
      this.form.controls.plan.valueChanges,
      this.form.controls.fact.valueChanges,
      this.form.controls.content.valueChanges.pipe(throttleTime(250))
    ).pipe(
      takeUntilDestroyed(this._destroyRef),
      map(() => this._id$.value ? this._update() : this._create()),
      exists(),
      switchMap((observable) => observable.pipe(tap((lesson) => {
        if (this._id$.value !== lesson.id) {
          this._id$.next(lesson.id);
        }
      })))
    ).subscribe()
  }

  private _bindForm(comment: IComment): void {
    this.form.patchValue({
      content: comment.content,
      fact: comment.factDate,
      plan: comment.planDate
    }, {emitEvent: false});
  }

  private _create(): Observable<IComment> | null {
    const form = this.form.getRawValue();

    const request: ICommentCreateDto = {
      eventId: this._eventId,
      content: form.content,
      planDate: form.plan,
      factDate: form.fact,
    };

    this._commentSubscription.unsubscribe();

    return this._eventsService.createComment(request);
  }

  private _update(): Observable<IComment> | null {
    const form = this.form.getRawValue();

    const request: ICommentUpdateDto = {
      id: required(this._id$.value),
      content: form.content,
      planDate: form.plan,
      factDate: form.fact
    };

    return this._eventsService.updateComment(request);
  }
}

export interface ICommentForm {
  content: FormControl<string>;
  plan: FormControl<number | null>;
  fact: FormControl<number | null>;
}
