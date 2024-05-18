import {MatDialogRef} from "@angular/material/dialog";
import {ReportsDetailDialogComponent} from "@ui/widgets";

export abstract class ReportsDialogService {
  public abstract openDetail(id: string): MatDialogRef<ReportsDetailDialogComponent>;
}
