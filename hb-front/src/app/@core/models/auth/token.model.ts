import {RoleEnum} from "@core/models";

export interface ITokenModel {
    id: string;
    role: RoleEnum;
    exp: number;
    iss: string;
    aud: string;
}
