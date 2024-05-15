import {MatDialogRef} from "@angular/material/dialog";
import {StatesCreateDialogComponent} from "@ui/containers";

export abstract class StatesDialogService {
  public abstract openCreate(): MatDialogRef<StatesCreateDialogComponent>;
}
