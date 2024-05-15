import {ITokenModel} from "@core/models";
import {Injectable} from "@angular/core";
import {AuthState} from "@core/abstracts";

@Injectable()
export class AuthImplState extends AuthState<ITokenModel> {
  constructor() {
    super("JWT_TOKEN");
  }
}
