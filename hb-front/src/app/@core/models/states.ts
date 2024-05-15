import {IDepartment, IJob} from "@core/models";

export interface IState {
  readonly id: string;
  readonly count: number;
  readonly hours: number;

  readonly department: IDepartment;
  readonly job: IJob;

  readonly startDate: Date;
  readonly endDate: Date;
}
