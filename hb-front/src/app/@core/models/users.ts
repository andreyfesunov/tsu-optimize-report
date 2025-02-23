import { IState } from "@core/dtos";

export enum RoleEnum {
  USER = 1,
  ADMIN = 2
}

export interface ITokenModel {
  readonly id: string;
  readonly role: RoleEnum;
  readonly firstname: string;
  readonly lastname: string;
  readonly exp: number;
  readonly iss: string;
  readonly aud: string;
}

export interface IUser {
  readonly id: string;
  readonly role: RoleEnum;
  readonly firstname: string;
  readonly lastname: string;
  readonly email: string;
}

export interface IUserState {
  readonly user: IUser & {
    readonly rank: string | null;
    readonly degree: string | null;
  };
  readonly states: IState[];
  expanded?: boolean;
}

