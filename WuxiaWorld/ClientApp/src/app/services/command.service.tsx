import { Commands } from '../models/inputbox';
import { Action, AuthAction } from '../models/action.interface';
import { TerminalCommands } from '../models/command.interface';

export default function(novelDispatch: any, authDispatch: any, setJwtToken: any) {
    return {
        list: {
            method: (args: any, print: any) => {
                const inputValue = args._[0];
                if (inputValue === 'genres') {
                    novelDispatch({
                        type: Commands.SendCommand,
                        inputValue: inputValue,
                        print: print,
                    } as Action);
                } else {
                    print('Command not valid');
                }
            },
        },
        login: {
            method: (args: any, print: any) => {
                const inputs = args._;
                if (inputs.length === 2) {
                    authDispatch({
                        type: Commands.Login,
                        username: inputs[0],
                        password: inputs[1],
                        print: print,
                        setJwtToken: setJwtToken,
                    } as AuthAction);
                } else {
                    print('Invalid input. Please provide username and password properly');
                }
            },
        },
    } as TerminalCommands;
}
