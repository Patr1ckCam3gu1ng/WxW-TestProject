import * as React from 'react';

interface Props {
    message: string;
}

export default class ComponentMessage extends React.Component<Props> {
    render = () => {
        return (
            <div>
                <h4>{this.props.message}</h4>
            </div>
        );
    };
}
