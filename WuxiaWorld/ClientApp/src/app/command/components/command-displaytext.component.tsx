import * as React from 'react';
import { State } from '../models/command-state.interface';
import { Genres } from '../models/genres.interface';

interface Props {
    state: State;
}

export default class CommandDisplayText extends React.Component<Props> {
    render() {
        return (
            <div>
                <h2>Enter CLI command</h2>
                {this.props.state?.genres?.map((value: Genres, key) => {
                    return (
                        <h2 key={key}>
                            {(key + 1).toString()}.) {value.name}
                        </h2>
                    );
                })}
            </div>
        );
    }
}
