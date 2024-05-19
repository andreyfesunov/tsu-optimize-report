import {MatDialogRef} from "@angular/material/dialog";
import {ReportsDetailDialogComponent, ReportsStartDialogComponent} from "@ui/widgets";

export abstract class ReportsDialogService {
  public abstract openDetail(id: string): MatDialogRef<ReportsDetailDialogComponent>;

  public abstract openStart(): MatDialogRef<ReportsStartDialogComponent, FormData>;
}
