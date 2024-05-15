import {IInstitute} from "@core/models";

export interface IDepartment {
  readonly id: string;
  readonly institute: IInstitute;
  readonly name: string;
}
