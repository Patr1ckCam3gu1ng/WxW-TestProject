import React from 'react';

import CommandInputBox from '../components/terminal.component';
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
                        <CommandInputBox />
                    </ChapterContextProvider>
                </GenreContextProvider>
            </NovelContextProvider>
        </AuthContextProvider>
    );
}

export default TerminalContainer;
