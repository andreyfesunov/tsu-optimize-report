import {IUser} from "@core/models";
import {AuthState} from "@core/abstracts/states/auth.state";

export class AuthImplState extends AuthState<IUser> {
    constructor() {
        super("JWT_TOKEN");
    }
}
