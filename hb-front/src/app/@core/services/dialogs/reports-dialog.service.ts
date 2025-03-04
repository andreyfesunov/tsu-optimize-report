import {inject, Injectable} from "@angular/core";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {ReportsStartDialogComponent} from "@ui/widgets";

@Injectable({providedIn: "root"})
export class ReportsDialogService {
  private readonly _dialog = inject(MatDialog);

  public openStart(): MatDialogRef<ReportsStartDialogComponent, FormData> {
    return this._dialog.open(ReportsStartDialogComponent, {minWidth: '600px'});
  }
}
