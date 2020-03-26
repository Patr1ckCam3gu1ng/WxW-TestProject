import React, { useContext } from 'react';
import Terminal from 'terminal-in-react';

import { AuthContext } from '../contexts/auth.context';
import { ChapterContext } from '../contexts/chapter.context';
import { NovelContext } from '../contexts/novel.context';
import { GenreContext } from '../contexts/genre.context';

import commands from '../services/command.service';

import { CommonContext } from '../models/commonContext.inteface';
import { GenreReducerContext } from '../models/genreReducerContext';
import { AuthReducerContext } from '../models/authContext.interface';

const TerminalComponent = () => {
    const { dispatch: genreDispatch } = useContext(GenreContext) as GenreReducerContext;
    const { setJwtToken, dispatch: authDispatch } = useContext(AuthContext) as AuthReducerContext;
    const { dispatch: chapterDispatch } = useContext(ChapterContext) as CommonContext;
    const { dispatch: novelDispatch } = useContext(NovelContext) as CommonContext;

    return (
        <div
            style={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100vh',
                fontSize: '1.3em',
            }}
        >
            <Terminal
                color="green"
                backgroundColor="black"
                barColor="black"
                style={{ fontWeight: 'bold', fontSize: '1em' }}
                commands={commands(genreDispatch, authDispatch, chapterDispatch, novelDispatch, setJwtToken)}
                msg={'Welcome to WuxiaWorld. Enter the command to begin:'}
                allowTabs={false}
            />
        </div>
    );
};

export default TerminalComponent;
