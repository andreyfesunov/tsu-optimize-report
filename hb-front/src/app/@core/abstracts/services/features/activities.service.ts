import {Observable} from "rxjs";
import {IActivity} from "@core/models";

export abstract class ActivitiesService {
  public abstract getAll(): Observable<IActivity[]>;
}
