import {AuthState, IUser} from "../../../shared";

export class AuthStateImpl extends AuthState<IUser> {
  constructor() {
    super('JWT_TOKEN');
  }
}
