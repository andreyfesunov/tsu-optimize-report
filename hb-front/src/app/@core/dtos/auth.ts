export interface ITokenDto {
  token: string
}

export interface ILoginDto {
  email: string;
  password: string;
}

export interface IRegDto extends ILoginDto {
  firstname: string;
  lastname: string;
}
