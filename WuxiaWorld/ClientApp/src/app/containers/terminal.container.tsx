import React from 'react';

import CommandInputBox from '../components/terminal.component';
import GenreContextProvider from '../contexts/genre.context';
import AuthContextProvider from '../contexts/auth.context';

function TerminalContainer() {
    return (
        <AuthContextProvider>
            <GenreContextProvider>
                <CommandInputBox />
            </GenreContextProvider>
        </AuthContextProvider>
    );
}

export default TerminalContainer;
