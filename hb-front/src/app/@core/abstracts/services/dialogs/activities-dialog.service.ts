import {MatDialogRef} from "@angular/material/dialog";
import {ActivitiesEventDialogComponent} from "@ui/widgets";

export abstract class ActivitiesDialogService {
  public abstract openAddEvent(activityId: string, activityName: string): MatDialogRef<ActivitiesEventDialogComponent>;
}
