import {BehaviorSubject, map, Observable, shareReplay} from "rxjs";
import {IPagination} from "@core/models";
import {IPaginationRequest} from "@core/dtos";
import {Spinner, switchMapSpinner} from "@core/utils";

export interface ITableConfig {
  request: IPaginationRequest;
  // columns: ITableColumn[];
}

export abstract class TableController<TEntity> {
  protected readonly spinner: Spinner = new Spinner();

  protected abstract config(): ITableConfig;

  protected abstract load(request: IPaginationRequest): Observable<IPagination<TEntity>>;

  protected readonly request$: BehaviorSubject<IPaginationRequest> = new BehaviorSubject<IPaginationRequest>(this.config().request);

  protected readonly page$: Observable<IPagination<TEntity>> = this.request$.pipe(
    switchMapSpinner((request) => this.load(request), this.spinner),
    shareReplay({
      bufferSize: 1,
      refCount: true
    })
  );

  protected readonly items$: Observable<TEntity[]> = this.page$.pipe(map((page) => page.entities));
}
