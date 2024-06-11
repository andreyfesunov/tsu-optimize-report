import {IWork} from "@core/models/works";

export interface IEventType {
  readonly id: string;
  readonly name: string;
  readonly description: string;
  readonly work: IWork;
}
