import {IInstitute} from "@core/models/institutes";

export interface IDepartment {
    readonly id: string;
    readonly institute: IInstitute;
    readonly name: string;
}
