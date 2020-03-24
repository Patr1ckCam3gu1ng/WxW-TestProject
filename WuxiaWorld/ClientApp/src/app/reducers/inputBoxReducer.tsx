import { Commands, InputboxAction, InputboxInterface } from '../models/inputbox.interface';
import Terminal from 'terminal-in-react';
import React from 'react';

export const InputBoxReducer = (state: InputboxInterface, action: InputboxAction) => {
    switch (action.type) {
        case Commands.SendCommand: {
            console.log('hello', action);
            action.print('querty');
        }
    }

    return state;
};
