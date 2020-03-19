import * as React from 'react';
import { Genres } from '../models/genres.interface';

interface Props {
    genres: Genres[];
}

export default class ComponentGender extends React.Component<Props> {
    render = () => (
        <div>
            {this.props.genres?.map((value: Genres, key) => {
                return (
                    <h2 key={key}>
                        {(key + 1).toString()}.) {value.name}
                    </h2>
                );
            })}
        </div>
    );
}
