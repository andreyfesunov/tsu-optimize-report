import {IDepartment} from "@core/models/departments";
import {IJob} from "@core/models/jobs";

export interface IState {
    readonly id: string;
    readonly count: number;
    readonly hours: number;

    readonly department: IDepartment;
    readonly job: IJob;

    readonly startDate: Date;
    readonly endDate: Date;
}
