import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IPagination, IReport} from "@core/models";

export abstract class ReportService {
    public abstract search(dto: IPaginationRequest): Observable<IPagination<IReport>>;
}
