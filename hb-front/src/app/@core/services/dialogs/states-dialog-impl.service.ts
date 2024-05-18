import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {Injectable} from "@angular/core";
import {StatesDialogService} from "@core/abstracts";
import {StatesCreateDialogComponent} from "@ui/widgets";

@Injectable()
export class StatesDialogImplService extends StatesDialogService {
  constructor(
    private readonly _dialog: MatDialog
  ) {
    super();
  }

  public openCreate(): MatDialogRef<StatesCreateDialogComponent> {
    return this._dialog.open(StatesCreateDialogComponent, {minWidth: '600px'});
  }
}
