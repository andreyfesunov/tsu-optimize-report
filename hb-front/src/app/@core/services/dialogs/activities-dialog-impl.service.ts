import {Injectable} from "@angular/core";
import {ActivitiesDialogService} from "@core/abstracts";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {ActivitiesEventDialogComponent, IActivitiesEventDialogData} from "@ui/widgets";

@Injectable()
export class ActivitiesDialogImplService extends ActivitiesDialogService {
  constructor(
    private readonly _dialog: MatDialog
  ) {
    super();
  }

  public openAddEvent(activityId: string, activityName: string): MatDialogRef<ActivitiesEventDialogComponent> {
    return this._dialog.open<ActivitiesEventDialogComponent, IActivitiesEventDialogData>(ActivitiesEventDialogComponent, {
      data: {
        activityId,
        activityName
      }
    });
  }
}
