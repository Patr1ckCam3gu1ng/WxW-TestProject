import { AuthAction } from '../models/action.interface';
import { Commands } from '../models/inputbox';
import { UserAccount } from '../models/userAccount.interface';
import apis from '../api';
import { ApiError } from '../models/apiError.interface';

export const AuthReducer = (state: string, action: AuthAction): string => {
    switch (action.type) {
        case Commands.Login: {
            apis.auth()
                .login({
                    username: action.username,
                    password: action.password,
                } as UserAccount)
                .then(({ data }) => {
                    action.setJwtToken(`Bearer ${data}`);
                    action.print(`Login successful. Welcome ${action.username}`);
                    return data;
                })
                .catch(function(error: ApiError) {
                    action.print(error.message);
                });
        }
    }

    return state;
};
