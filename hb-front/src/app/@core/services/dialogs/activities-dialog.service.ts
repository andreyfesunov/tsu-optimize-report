import {Injectable, inject} from "@angular/core";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {ActivitiesEventDialogComponent, IActivitiesEventDialogData} from "@ui/widgets";
import {IActivitiesAssignEventRequest} from "@core/dtos";

@Injectable({providedIn: "root"})
export class ActivitiesDialogService {
  private readonly _dialog = inject(MatDialog);

  public openAssign(activityId: string, activityName: string): MatDialogRef<ActivitiesEventDialogComponent, IActivitiesAssignEventRequest> {
    return this._dialog.open<ActivitiesEventDialogComponent, IActivitiesEventDialogData, IActivitiesAssignEventRequest>(ActivitiesEventDialogComponent, {
      data: {
        activityId,
        activityName
      },
      minWidth: '600px'
    });
  }
}
