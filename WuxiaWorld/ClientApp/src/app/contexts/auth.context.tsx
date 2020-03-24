import React, { createContext, useReducer, useState, useEffect } from 'react';
import { AuthReducer } from '../reducers/auth.reducer';

export const AuthContext = createContext({});

const NovelContextProvider = (props: any): any => {
    const [jwtToken, setJwtToken] = useState('');
    const [state, dispatch] = useReducer(AuthReducer, jwtToken);

    useEffect(() => {
        localStorage.setItem('jwtToken', jwtToken);
    });

    return (
        <AuthContext.Provider value={{ jwtToken, setJwtToken, dispatch }}>{props.children}</AuthContext.Provider>
    );
};

export default NovelContextProvider;
