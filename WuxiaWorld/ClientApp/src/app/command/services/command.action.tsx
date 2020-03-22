import api from './command.api';
import { Genre } from '../../models/genre.interface';
import { UserAccount } from '../../models/userAccount.interface';
import { Actions } from '../../models/actions.interface';
import { ApiError } from '../../models/apiError.interface';
import error from '../services/command.error';
import { Novel } from '../../models/novel.interface';

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
    clearInput: (actionSetState: Actions): void => {
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
                    .then((genres: Genre[]) => {
                        actionSetState.setStateListGenres(genres);
                    })
                    .catch((apiError: ApiError) => {
                        error.notAuthenticated(apiError.code, actionSetState);
                    });
            }
            if (commandType === 'novels') {
                api.get
                    .novels(commandType, actions.authenticationHeader())
                    .then((novels: Novel[]) => {
                        actionSetState.setStateListNovels(novels);
                    })
                    .catch((apiError: ApiError) => {
                        error.notAuthenticated(apiError.code, actionSetState);
                    });
            }

            clearInput();
            return;
        }
        if (firstCommand === 'login') {
            const userAccount = actions.getSecondThirdCommand(value);
            api.login(userAccount)
                .then((newToken: string | any) => {
                    localStorage.setItem('jwtToken', newToken);
                    actionSetState.setMessage(`Login successful. Welcome ${userAccount.username}`);
                })
                .catch((apiError: ApiError) => {
                    error.invalidCredential(apiError.code, actionSetState);
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
            actionSetState.setStateEmpty().setMessage('Logout successfully');
            localStorage.clear();
            clearInput();
            return;
        }

        actionSetState.setMessage('Command not recognize');
    },
};
