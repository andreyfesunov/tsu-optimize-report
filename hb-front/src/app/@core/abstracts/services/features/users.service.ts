import {IPaginationRequest} from "@core/dtos";
import {Observable} from "rxjs";
import {IPagination, IUser} from "@core/models";

export abstract class UsersService {
  public abstract search(dto: IPaginationRequest): Observable<IPagination<IUser>>;
}
