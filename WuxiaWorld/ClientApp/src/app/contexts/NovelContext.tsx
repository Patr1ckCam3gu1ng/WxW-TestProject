import React, { Component, createContext } from 'react';
import { NovelState } from '../models/novelState.interface';

export const NovelContext = createContext<Partial<NovelState>>({});

class NovelContextProvider extends Component {
    state: NovelState = {
        genres: [],
        novels: [],
        inputValue: '',
        message: '',
    };

    render() {
        return <NovelContext.Provider value={{ ...this.state }}>{this.props.children}</NovelContext.Provider>;
    }
}

export default NovelContextProvider;
