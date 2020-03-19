import api from './command.api';
import { Genres } from '../models/genres.interface';
import { UserAccount } from '../models/userAccount.interface';
import { Actions } from '../models/actions.interface';
import { ApiError } from '../models/apiError.interface';

const actions = {
    getFirstCommand: (value: string) => {
        if (value.trim() !== '') {
            return value.split(' ')[0];
        }
    },
    getCommandType: (value: string) => {
        if (value.trim() !== '') {
            const splitValue = value.split(' ');
            if (splitValue.length > 1) {
                return splitValue[1];
            }
        }
    },
    getSecondThirdCommand: (value: string): UserAccount | any => {
        if (value.trim() !== '') {
            const splitValue = value.split(' ');
            if (splitValue.length > 1) {
                return {
                    username: splitValue[1],
                    password: splitValue[2],
                };
            }
        }
    },
    authenticationHeader: (): string | any => {
        return `Bearer ${localStorage.getItem('jwtToken')}`;
    },
    clearInput: (actionSetState: Actions) => {
        actionSetState.setInputValue('');
    },
};
export default {
    onEnter: (value: string, actionSetState: Actions): void => {
        const firstCommand = actions.getFirstCommand(value);

        const clearInput = (): void => {
            actions.clearInput(actionSetState);
        };

        if (firstCommand === 'list') {
            const commandType = actions.getCommandType(value);
            if (commandType === 'genres') {
                api.get
                    .genres(commandType, actions.authenticationHeader())
                    .then((genres: Genres[]) => {
                        actionSetState.setStateListGenres(genres);
                    })
                    .catch((error: ApiError) => {
                        if (error.code === 401) {
                            actionSetState.setMessage('Invalid username or password');
                        }
                    });
                clearInput();
                return;
            }
        }
        if (firstCommand === 'login') {
            const userAccount = actions.getSecondThirdCommand(value);
            api.login(userAccount).then((newToken: string | any) => {
                localStorage.setItem('jwtToken', newToken);
            });
            clearInput();
            return;
        }
        if (firstCommand === 'clear') {
            actionSetState.setStateEmpty();
            clearInput();
            return;
        }
        if (firstCommand === 'logout') {
            actionSetState.setStateEmpty();
            localStorage.clear();
            clearInput();
            return;
        }

        actionSetState.setMessage('Command not recognize');
    },
};
