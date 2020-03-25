import { Action, AuthAction } from '../models/action.interface';
import apis from '../api';
import { UserAccount } from '../models/userAccount.interface';
import { ApiError } from '../models/apiError.interface';

export default {
    jwtToken: (action: Action): string => {
        const jwtToken = localStorage.getItem('jwtToken')?.toString() || '';
        if (jwtToken === '') {
            action.print('User not yet authenticated. Please login');
        }
        return jwtToken;
    },
    login: (action: AuthAction): void => {
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
    },
};
