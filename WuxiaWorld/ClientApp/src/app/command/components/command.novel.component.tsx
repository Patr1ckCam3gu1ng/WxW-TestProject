import * as React from 'react';
import { Novel } from '../../models/novel.interface';

interface Props {
    novel: Novel[];
}

export default class ComponentNovel extends React.Component<Props> {
    render = () => (
        <div>
            {this.props.novel?.length > 0 ? (
                this.props.novel?.map((novel: Novel, key) => {
                    return (
                        <div key={key}>
                            <h4>Name: {novel.name}</h4>
                            <h4>Synopsis: {novel.synopsis}</h4>
                            <h4>Created on: {novel.timeCreated}</h4>
                        </div>
                    );
                })
            ) : (
                <h4>No novels in the database</h4>
            )}
        </div>
    );
}
