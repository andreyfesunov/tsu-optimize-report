export enum RoleEnum {
  USER = 1,
  ADMIN = 2
}

export interface ITokenModel {
  id: string;
  role: RoleEnum;
  exp: number;
  iss: string;
  aud: string;
}

export interface IUser {
  id: string;
  role: RoleEnum;
  firstname: string;
  lastname: string;
  email: string;
}
