import React, { useContext } from 'react';
import Terminal from 'terminal-in-react';

import { GenreContext } from '../contexts/genre.context';
import { AuthContext } from '../contexts/auth.context';
import { ChapterContext } from '../contexts/chapter.context';
import { NovelContext } from '../contexts/novel.context';

import commands from '../services/command.service';
import { authContext } from '../models/authContext.interface';
import { CommonContext } from '../models/commonContext.inteface';

const TerminalComponent = () => {
    // @ts-ignore
    const { dispatch: genreDispatch } = useContext(GenreContext);
    const { setJwtToken, dispatch: authDispatch } = useContext(AuthContext) as authContext;
    const { dispatch: chapterDispatch } = useContext(ChapterContext) as CommonContext;
    const { dispatch: novelDispatch } = useContext(NovelContext) as CommonContext;

    return (
        <div
            style={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100vh',
            }}
        >
            <Terminal
                color="green"
                backgroundColor="black"
                barColor="black"
                style={{ fontWeight: 'bold', fontSize: '1em' }}
                commands={commands(genreDispatch, authDispatch, chapterDispatch, novelDispatch, setJwtToken)}
                msg={'Welcome to WuxiaWold. Enter command to begin:'}
                allowTabs={false}
            />
        </div>
    );
};

export default TerminalComponent;
