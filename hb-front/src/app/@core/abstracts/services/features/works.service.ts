import {Observable} from "rxjs";
import {IWork} from "@core/models";

export abstract class WorksService {
  public abstract getAll(): Observable<IWork[]>;
}
