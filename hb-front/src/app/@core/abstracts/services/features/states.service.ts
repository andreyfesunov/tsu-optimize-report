import {IPaginationRequest, IStateCreateRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IPagination, IState} from "@core/models";

export abstract class StatesService {
  public abstract search(dto: IPaginationRequest): Observable<IPagination<IState>>;

  public abstract create(dto: IStateCreateRequest): Observable<boolean>;
}
