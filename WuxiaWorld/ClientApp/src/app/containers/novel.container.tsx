import React, { Component } from 'react';

import CommandInputBox from '../command/components/command.inputbox.component';
import AuthContextProvider from '../contexts/AuthContext';
import NovelContextProvider from '../contexts/NovelContext';

class NovelContainer extends Component {
    render() {
        return (
            <AuthContextProvider>
                <NovelContextProvider>
                    <CommandInputBox />
                </NovelContextProvider>
            </AuthContextProvider>
        );
    }
}

export default NovelContainer;
