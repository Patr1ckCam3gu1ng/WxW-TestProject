import * as React from 'react';
import { Genres } from '../models/genres.interface';

interface Props {
    onEnter: (value: string, commandSetState: (response: Genres[]) => void) => void;
    commandSetState: (response: Genres[]) => void;
    setInputValue: (value: string) => void;
    inputValue: string;
}

export default class CommandInputbox extends React.Component<Props> {
    render() {
        return (
            <div>
                <input
                    type={'text'}
                    onKeyUp={(event: React.KeyboardEvent<HTMLInputElement>) => {
                        if (event.keyCode === 13) {
                            this.props.onEnter(event.currentTarget.value, this.props.commandSetState);
                        }
                    }}
                    onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                        this.props.setInputValue(event.currentTarget.value);
                    }}
                    value={this.props.inputValue}
                />
            </div>
        );
    }
}
