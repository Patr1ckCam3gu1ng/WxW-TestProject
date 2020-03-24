import React from 'react';

import CommandInputBox from '../command/components/command.inputbox.component';
import AuthContextProvider from '../contexts/AuthContext';
import NovelContextProvider from '../contexts/NovelContext';

function NovelContainer() {
    return (
        <AuthContextProvider>
            <NovelContextProvider>
                <CommandInputBox />
            </NovelContextProvider>
        </AuthContextProvider>
    );
}

export default NovelContainer;
