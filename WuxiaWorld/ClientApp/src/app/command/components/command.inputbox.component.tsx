import * as React from 'react';
import { Actions } from '../models/actions.interface';

interface Props {
    onEnter: (value: string, actionSetState: Actions) => void;
    actionSetState: Actions;
    setInputValue: (value: string) => void;
    inputValue: string;
}

export default class CommandInputBox extends React.Component<Props> {
    render = () => (
        <div>
            <input
                type={'text'}
                onKeyUp={(event: React.KeyboardEvent<HTMLInputElement>) => {
                    if (event.keyCode === 13) {
                        this.props.onEnter(event.currentTarget.value, this.props.actionSetState);
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
