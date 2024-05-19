import {MatDialogRef} from "@angular/material/dialog";
import {ActivitiesEventDialogComponent} from "@ui/widgets";
import {IActivitiesAssignEventRequest} from "@core/dtos";

export abstract class ActivitiesDialogService {
  public abstract openAddEvent(activityId: string, activityName: string): MatDialogRef<ActivitiesEventDialogComponent, IActivitiesAssignEventRequest>;
}
