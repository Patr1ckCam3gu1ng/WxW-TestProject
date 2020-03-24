import { Commands, InputboxAction, InputboxInterface } from '../models/inputbox.interface';

export const InputBoxReducer = (state: InputboxInterface, action: InputboxAction) => {
    switch (action.type) {
        case Commands.SendCommand: {
            console.log('hello', action);
        }
    }

    return state;
};
