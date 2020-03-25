import { Action } from '../models/action.interface';

export default {
    create: (action: Action): void => {
        action.runCommand('clear');
        action.runCommand('list genres');
        action.print('Enter the Novel id to assign this chapter:');
        return;
    },
};
