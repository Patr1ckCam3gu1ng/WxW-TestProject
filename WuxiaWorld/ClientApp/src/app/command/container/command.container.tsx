import * as React from 'react';

import { Genre } from '../models/genre.interface';
import { Actions } from '../models/actions.interface';
import { Novel } from '../models/novel.interface';
import { CommandState } from '../models/command-state.interface';
import event from '../services/command.action';
import ComponentGender from '../components/command.genre.component';
import CommandInputBox from '../components/command.inputbox.component';
import ComponentMessage from '../components/command.message.component';
import ComponentNovel from '../components/command.novel.component';

interface Props {}

export default class CommandContainer extends React.Component<Props, CommandState> {
    state: CommandState = {
        genres: [],
        novels: [],
        inputValue: '',
        message: '',
    };

    actions: Actions = {
        setStateListGenres: (genres: Genre[]): void => {
            this.setState((state: CommandState) => {
                return {
                    ...state,
                    genres: genres,
                    novels: [],
                    message: '',
                    inputValue: '',
                };
            });
        },
        setStateListNovels: (novels: Novel[]): void => {
            this.setState((state: CommandState) => {
                return {
                    ...state,
                    message: '',
                    inputValue: '',
                    genres: [],
                    novels: novels,
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
        setStateEmpty: (): Actions => {
            this.setState((state: CommandState) => {
                return {
                    ...state,
                    inputValue: '',
                    message: '',
                    genres: [],
                    novels: []
                };
            });
            return this.actions;
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
                <ComponentNovel novel={this.state.novels} />
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
