import * as React from 'react';
import { Genres } from '../models/genres.interface';

import CommandInputbox from './command-inputbox.component';
import event from '../services/command.action';
import CommandDisplayText from './command-displaytext.component';
import { State } from '../models/command-state.interface';

interface Props {}

export default class CommandComponent extends React.Component<Props, State> {
    state: State = {
        genres: [],
        inputValue: '',
    };

    actions = {
        commandSetState: (response: Genres[]): void => {
            this.setState(state => {
                return {
                    ...state,
                    genres: response,
                    inputValue: '',
                };
            });
        },
        setInputValue: (value: string): void => {
            this.setState(state => {
                return {
                    ...state,
                    inputValue: value,
                };
            });
        },
    };

    render() {
        return (
            <div>
                <CommandDisplayText state={this.state} />
                <CommandInputbox
                    onEnter={event.onEnter}
                    commandSetState={this.actions.commandSetState}
                    setInputValue={this.actions.setInputValue}
                    inputValue={this.state.inputValue}
                />
            </div>
        );
    }
}
