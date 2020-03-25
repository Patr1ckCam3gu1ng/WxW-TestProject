import { AuthAction } from '../models/action.interface';
import { Commands } from '../models/inputbox';
import authService from '../services/auth.service';

export const authReducer = (state: string, action: AuthAction): string => {
    switch (action.type) {
        case Commands.Logout: {
            action.print('Logout successfully');
            localStorage.clear();
            break;
        }
        case Commands.Login: {
            authService.login(action);
            break;
        }
    }
    return state;
};
