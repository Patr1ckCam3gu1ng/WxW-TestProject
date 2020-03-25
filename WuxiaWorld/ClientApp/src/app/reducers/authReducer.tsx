import authService from '../services/auth.service';
import { Commands } from '../models/inputbox';
import { AuthAction } from '../models/action.interface';

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
