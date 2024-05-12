import {IState} from "@core/models/states";

export interface IReport {
    readonly state: IState;
    readonly rate: number;
    readonly status: string;
}
