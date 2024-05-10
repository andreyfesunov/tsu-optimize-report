import {ITokenModel} from "@core/models";
import {AuthState} from "@core/abstracts/states/auth.state";
import {Injectable} from "@angular/core";

@Injectable()
export class AuthImplState extends AuthState<ITokenModel> {
    constructor() {
        super("JWT_TOKEN");
    }
}
