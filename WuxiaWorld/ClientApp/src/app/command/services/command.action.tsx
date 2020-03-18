import api from '../services/command.api';
import { Genres } from '../models/genres.interface';

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
};
export default {
    authenticationHeader: (): string => {
        return `Bearer ${localStorage.getItem('jwtToken')}`;
    },
    onEnter: (value: string, commandSetState: (response: Genres[] | any) => void): void => {
        const firstCommand = actions.getFirstCommand(value);

        if (firstCommand === 'list') {
            const commandType = actions.getCommandType(value);
            if (commandType === 'genres') {
                api.get(commandType).then((response: Genres[] | any) => {
                    commandSetState(response);
                });
            }
        }
        if (firstCommand === 'clear') {
            commandSetState([]);
        }
    },
};
