import {Observable} from "rxjs";
import {IInstitute} from "@core/models";

export abstract class InstitutesService {
  public abstract getAll(): Observable<IInstitute[]>;
}
