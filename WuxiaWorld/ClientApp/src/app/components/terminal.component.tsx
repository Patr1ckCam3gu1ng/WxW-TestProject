import React, { useContext } from 'react';
import Terminal from 'terminal-in-react';
import { GenreContext } from '../contexts/genre.context';
import { AuthContext } from '../contexts/auth.context';
import { authContext } from '../models/authContext.interface';
import commands from '../services/command.service';

const TerminalComponent = () => {
    // @ts-ignore
    const { dispatch: genreDispatch } = useContext(GenreContext);
    const { setJwtToken, dispatch: authDispatch } = useContext(AuthContext) as authContext;

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
                commands={commands(genreDispatch, authDispatch, setJwtToken)}
                msg={'Welcome to WuxiaWold. Enter command to begin:'}
                allowTabs={false}
            />
        </div>
    );
};

export default TerminalComponent;
