import {MatDialogRef} from "@angular/material/dialog";
import {StatesAssignDialogComponent, StatesCreateDialogComponent} from "@ui/widgets";
import {IStateAssignRequest, IStateCreateRequest} from "@core/dtos";

export abstract class StatesDialogService {
  public abstract openCreate(): MatDialogRef<StatesCreateDialogComponent, IStateCreateRequest>;

  public abstract openAssign(id: string): MatDialogRef<StatesAssignDialogComponent, IStateAssignRequest>;
}
