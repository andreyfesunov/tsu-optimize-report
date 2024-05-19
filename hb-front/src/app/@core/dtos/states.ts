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
