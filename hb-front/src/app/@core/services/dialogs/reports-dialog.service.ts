import {Injectable, inject} from "@angular/core";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {IReportsDetailDialogData, ReportsDetailDialogComponent, ReportsStartDialogComponent} from "@ui/widgets";

@Injectable({providedIn: "root"})
export class ReportsDialogService {
  private readonly _dialog = inject(MatDialog);

  public openDetail(id: string): MatDialogRef<ReportsDetailDialogComponent> {
    return this._dialog.open<ReportsDetailDialogComponent, IReportsDetailDialogData>(ReportsDetailDialogComponent, {
      minWidth: '900px',
      data: {id: id}
    });
  }

  public openStart(): MatDialogRef<ReportsStartDialogComponent, FormData> {
    return this._dialog.open(ReportsStartDialogComponent, {minWidth: '600px'});
  }
}
