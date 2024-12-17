import {Injectable, inject} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {
  ICommentCreateDto,
  ICommentUpdateDto,
  IEventCreateDto,
  IEventUpdateDto,
  ILessonCreateDto,
  ILessonUpdateDto
} from "@core/dtos";
import {Observable, shareReplay} from "rxjs";
import {IComment, IEvent, ILesson, ILessonType} from "@core/models";
import { environment } from "src/environments/environment";

@Injectable({providedIn: "root"})
export class EventsService {
  private readonly _apiRoot = environment.apiRoot;
  private readonly _httpClient = inject(HttpClient);

  private readonly _lessonTypesMemo: { [key: string]: Observable<ILessonType[]> } = {};

  public create(dto: IEventCreateDto): Observable<IEvent> {
    return this._httpClient.post<IEvent>(`${this._apiRoot}/Event`, dto);
  }

  public update(dto: IEventUpdateDto): Observable<IEvent> {
    return this._httpClient.put<IEvent>(`${this._apiRoot}/Event`, dto);
  }

  public delete(id: string): Observable<boolean> {
    return this._httpClient.delete<boolean>(`${this._apiRoot}/Event/${id}`);
  }

  public getLessonTypes(reportId: string): Observable<ILessonType[]> {
    return (reportId in this._lessonTypesMemo) ?
      this._lessonTypesMemo[reportId] :
      this._lessonTypesMemo[reportId] =
        this._httpClient.get<ILessonType[]>(`${this._apiRoot}/LessonType/${reportId}`).pipe(shareReplay(1));
  }

  public createLesson(dto: ILessonCreateDto): Observable<ILesson> {
    return this._httpClient.post<ILesson>(`${this._apiRoot}/Lesson`, dto);
  }

  public updateLesson(dto: ILessonUpdateDto): Observable<ILesson> {
    return this._httpClient.put<ILesson>(`${this._apiRoot}/Lesson`, dto);
  }

  public deleteLesson(id: string): Observable<boolean> {
    return this._httpClient.delete<boolean>(`${this._apiRoot}/Lesson/${id}`);
  }

  public createComment(dto: ICommentCreateDto): Observable<IComment> {
    return this._httpClient.post<IComment>(`${this._apiRoot}/Comment`, dto);
  }

  public updateComment(dto: ICommentUpdateDto): Observable<IComment> {
    return this._httpClient.put<IComment>(`${this._apiRoot}/Comment`, dto);
  }

  public deleteComment(id: string): Observable<boolean> {
    return this._httpClient.delete<boolean>(`${this._apiRoot}/Comment/${id}`);
  }
}
