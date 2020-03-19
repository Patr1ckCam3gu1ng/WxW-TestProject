import * as React from 'react';
import { Genres } from '../models/genres.interface';

import CommandInputBox from '../components/command.inputbox.component';
import event from '../services/command.action';
import ComponentGender from '../components/command.genre.component';
import { CommandState } from '../models/command-state.interface';
import { Actions } from '../models/actions.interface';
import ComponentMessage from '../components/command.message.component';

interface Props {}

export default class CommandContainer extends React.Component<Props, CommandState> {
    state: CommandState = {
        genres: [],
        inputValue: '',
        message: '',
    };

    actions: Actions = {
        setStateListGenres: (response: Genres[]): void => {
            this.setState((state: CommandState) => {
                return {
                    ...state,
                    genres: response,
                    message: '',
                    inputValue: '',
                };
            });
        },
        setInputValue: (value: string): void => {
            this.setState((state: CommandState) => {
                return {
                    ...state,
                    inputValue: value,
                };
            });
        },
        setStateEmpty: (): void => {
            this.setState((state: CommandState) => {
                return {
                    ...state,
                    inputValue: '',
                    message: '',
                    genres: [],
                };
            });
        },
        setMessage: (value: string): void => {
            this.setState((state: CommandState) => {
                return {
                    ...state,
                    inputValue: '',
                    message: value,
                };
            });
        },
    };

    render() {
        return (
            <div>
                <h2>Enter CLI command</h2>
                <ComponentGender genres={this.state.genres} />
                <ComponentMessage message={this.state.message} />
                <CommandInputBox
                    onEnter={event.onEnter}
                    actionSetState={this.actions}
                    setInputValue={this.actions.setInputValue}
                    inputValue={this.state.inputValue}
                />
            </div>
        );
    }
}
