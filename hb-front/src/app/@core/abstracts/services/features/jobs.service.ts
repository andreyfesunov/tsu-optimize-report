import {Observable} from "rxjs";
import {IJob} from "@core/models";

export abstract class JobsService {
  public abstract list(): Observable<IJob[]>;
}
