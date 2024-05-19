import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IPagination, IReport} from "@core/models";

export abstract class ReportsService {
  public abstract search(dto: IPaginationRequest): Observable<IPagination<IReport>>;

  public abstract create(id: string, data: FormData): Observable<boolean>;
}
