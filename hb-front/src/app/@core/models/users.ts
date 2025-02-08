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
