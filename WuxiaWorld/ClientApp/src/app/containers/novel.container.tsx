import React from 'react';

import CommandInputBox from '../components/terminal.component';
import GenreContextProvider from '../contexts/genre.context';
import AuthContextProvider from '../contexts/auth.context';

function NovelContainer() {
    return (
        <AuthContextProvider>
            <GenreContextProvider>
                <CommandInputBox />
            </GenreContextProvider>
        </AuthContextProvider>
    );
}

export default NovelContainer;
