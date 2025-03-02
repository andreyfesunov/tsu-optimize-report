import { IDepartment, IJob } from "@core/models";

export interface IStateCreateRequest {
  hours: number;
  count: number;
  jobId: string;
  startDate: Date;
  endDate: Date;
}

export interface IStateAssignRequest {
  stateId: string;
  userId: string;
}

export interface IState {
  readonly id: string;
  readonly count: number;
  readonly hours: number;
  readonly startDate: Date;
  readonly endDate: Date;
  readonly department: IDepartment;
  readonly job: IJob;
}
