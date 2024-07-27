import {Injectable} from "@angular/core";
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

@Injectable({providedIn: "root"})
export class EventsService {
  constructor(
    private readonly _httpClient: HttpClient
  ) {
  }

  private readonly _lessonTypesMemo: { [key: string]: Observable<ILessonType[]> } = {};

  public create(dto: IEventCreateDto): Observable<IEvent> {
    return this._httpClient.post<IEvent>('/api/Event', dto);
  }

  public update(dto: IEventUpdateDto): Observable<IEvent> {
    return this._httpClient.put<IEvent>('/api/Event', dto);
  }

  public delete(id: string): Observable<boolean> {
    return this._httpClient.delete<boolean>(`/api/Event/${id}`);
  }

  public getLessonTypes(reportId: string): Observable<ILessonType[]> {
    return (reportId in this._lessonTypesMemo) ?
      this._lessonTypesMemo[reportId] :
      this._lessonTypesMemo[reportId] =
        this._httpClient.get<ILessonType[]>(`/api/LessonType/${reportId}`).pipe(shareReplay(1));
  }

  public createLesson(dto: ILessonCreateDto): Observable<ILesson> {
    return this._httpClient.post<ILesson>('/api/Lesson/create', dto);
  }

  public updateLesson(dto: ILessonUpdateDto): Observable<ILesson> {
    return this._httpClient.post<ILesson>('/api/Lesson/update', dto);
  }

  public deleteLesson(id: string): Observable<boolean> {
    return this._httpClient.delete<boolean>(`/api/Lesson/${id}`);
  }

  public createComment(dto: ICommentCreateDto): Observable<IComment> {
    return this._httpClient.post<IComment>('/api/Comment', dto);
  }

  public updateComment(dto: ICommentUpdateDto): Observable<IComment> {
    return this._httpClient.put<IComment>('/api/Comment', dto);
  }

  public deleteComment(id: string): Observable<boolean> {
    return this._httpClient.delete<boolean>(`/api/Comment/${id}`);
  }
}
