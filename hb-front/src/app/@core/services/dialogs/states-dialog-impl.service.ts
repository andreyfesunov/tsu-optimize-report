import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {Injectable} from "@angular/core";
import {StatesDialogService} from "@core/abstracts";
import {IStatesAssignDialogData, StatesAssignDialogComponent, StatesCreateDialogComponent} from "@ui/widgets";
import {IStateAssignRequest, IStateCreateRequest} from "@core/dtos";

@Injectable()
export class StatesDialogImplService extends StatesDialogService {
  constructor(
    private readonly _dialog: MatDialog
  ) {
    super();
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
