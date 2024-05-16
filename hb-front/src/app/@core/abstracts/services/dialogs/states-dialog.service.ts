import {MatDialogRef} from "@angular/material/dialog";
import {StatesCreateDialogComponent} from "@ui/widgets";
import {IStateCreateRequest} from "@core/dtos";

export abstract class StatesDialogService {
  public abstract openCreate(): MatDialogRef<StatesCreateDialogComponent, IStateCreateRequest>;
}
