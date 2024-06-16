import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {Injectable} from "@angular/core";
import {IStatesAssignDialogData, StatesAssignDialogComponent, StatesCreateDialogComponent} from "@ui/widgets";
import {IStateAssignRequest, IStateCreateRequest} from "@core/dtos";

@Injectable({providedIn: "root"})
export class StatesDialogService {
  constructor(
    private readonly _dialog: MatDialog
  ) {
  }

  public openCreate(): MatDialogRef<StatesCreateDialogComponent, IStateCreateRequest> {
    return this._dialog.open(StatesCreateDialogComponent, {minWidth: '600px'});
  }

  public openAssign(id: string): MatDialogRef<StatesAssignDialogComponent, IStateAssignRequest> {
    return this._dialog.open<StatesAssignDialogComponent, IStatesAssignDialogData, IStateAssignRequest>(StatesAssignDialogComponent, {
      minWidth: '600px',
      data: {stateId: id}
    })
  }
}