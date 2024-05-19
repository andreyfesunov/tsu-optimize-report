import {ReportsDialogService} from "@core/abstracts";
import {Injectable} from "@angular/core";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {IReportsDetailDialogData, ReportsDetailDialogComponent} from "@ui/widgets";

@Injectable()
export class ReportsDialogImplService extends ReportsDialogService {
  constructor(
    private readonly _dialog: MatDialog
  ) {
    super();
  }

  public openDetail(id: string): MatDialogRef<ReportsDetailDialogComponent> {
    return this._dialog.open<ReportsDetailDialogComponent, IReportsDetailDialogData>(ReportsDetailDialogComponent, {
      minWidth: '900px',
      data: {id: id}
    });
  }
}
