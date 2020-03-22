import * as React from 'react';
import { Genre } from '../../models/genre.interface';

interface Props {
    genres: Genre[];
}

export default class ComponentGender extends React.Component<Props> {
    render = () => (
        <div>
            {this.props.genres?.map((value: Genre, key) => {
                return (
                    <h4 key={key}>
                        {(key + 1).toString()}.) {value.name}
                    </h4>
                );
            })}
        </div>
    );
}
