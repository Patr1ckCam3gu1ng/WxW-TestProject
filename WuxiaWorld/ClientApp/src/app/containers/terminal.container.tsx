import React from 'react';

import TerminalCommand from '../components/terminal.component';
import GenreContextProvider from '../contexts/genre.context';
import AuthContextProvider from '../contexts/auth.context';
import ChapterContextProvider from '../contexts/chapter.context';
import NovelContextProvider from '../contexts/novel.context';

function TerminalContainer() {
    return (
        <AuthContextProvider>
            <NovelContextProvider>
                <GenreContextProvider>
                    <ChapterContextProvider>
                        <TerminalCommand />
                    </ChapterContextProvider>
                </GenreContextProvider>
            </NovelContextProvider>
        </AuthContextProvider>
    );
}

export default TerminalContainer;
