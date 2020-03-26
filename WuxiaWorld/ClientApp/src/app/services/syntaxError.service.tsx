import { Action } from '../models/action.interface';
import { ErrorMessage } from './throwError.service';

export default {
    print: (action: Action, displayText: string): void => {
        action.runCommand('clear');
        setTimeout(() => {
            action.print(ErrorMessage.InvalidSyntax);
            action.print(displayText);
        }, 100);
    },
};
