import {BehaviorSubject, map, Observable, shareReplay, switchMap} from "rxjs";
import {IPagination} from "@core/models";
import {IPaginationRequest} from "@core/dtos";

export interface ITableConfig {
  request: IPaginationRequest;
  // columns: ITableColumn[];
}

export abstract class TableController<TEntity> {
  protected readonly request$: BehaviorSubject<IPaginationRequest> = new BehaviorSubject<IPaginationRequest>(this.config().request);
  protected readonly page$: Observable<IPagination<TEntity>> = this.request$.pipe(
    switchMap((request) => this.load(request)),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );
  protected readonly items$: Observable<TEntity[]> = this.page$.pipe(map((page) => page.entities));

  protected abstract config(): ITableConfig;

  protected abstract load(request: IPaginationRequest): Observable<IPagination<TEntity>>;

  protected nextPage(): void {
    const req = this.request$.value;

    req.pageNumber += 1;

    this.request$.next(req);
  }

  protected prevPage(): void {
    const req = this.request$.value;

    req.pageNumber -= 1;

    this.request$.next(req);
  }
}
