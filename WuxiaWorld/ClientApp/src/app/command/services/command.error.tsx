import { Actions } from '../../models/actions.interface';

export default {
    invalidCredential: (errorCode: number, actionSetState: Actions): void => {
        if (errorCode === 401) {
            actionSetState.setMessage('Invalid username or password');
        }
    },
    notAuthenticated(code: number, actionSetState: Actions): void {
        if (code === 401) {
            actionSetState.setMessage('Not yet authenticated');
        }
    },
};
