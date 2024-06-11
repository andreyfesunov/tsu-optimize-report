export interface IEventCreateDto {
  readonly stateUserId: string;
  readonly eventTypeId: string;
  readonly startedAt: Date;
  readonly endedAt: Date;
}

export interface IEventUpdateDto {
  readonly id: string;
  readonly startedAt: Date;
  readonly endedAt: Date;
}
