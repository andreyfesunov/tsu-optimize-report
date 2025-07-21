import { IEvent, IState } from '@core/models';

export enum ReportStatus {
  NOT_ACTIVE = 0,
  ACTIVE = 1,
  FINISHED = 2,
}

export enum SemesterEnum {
  AUTUMN = 1,
  SPRING = 2,
}

export const reportStatusToString = (status: ReportStatus): string => {
  const map = {
    [ReportStatus.NOT_ACTIVE]: 'Неактивна',
    [ReportStatus.ACTIVE]: 'Активна',
    [ReportStatus.FINISHED]: 'Завершена',
  };

  return map[status];
};

export const reportStatusStyles = (status: ReportStatus) => ({
  'report-status': true,
  'report-status--active': status === ReportStatus.ACTIVE,
  'report-status--not-active': status === ReportStatus.NOT_ACTIVE,
  'report-status--finished': status === ReportStatus.FINISHED,
});

export interface IReportListItem {
  readonly id: string;
  readonly state: IState;
  readonly rate: number;
  readonly status: ReportStatus;
}

export interface IReportDetail extends IReportListItem {
  readonly events: readonly IEvent[];
}
