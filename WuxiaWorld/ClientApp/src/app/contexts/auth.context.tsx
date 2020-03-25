import React, { createContext, useReducer, useState, useEffect } from 'react';
import { authReducer } from '../reducers/authReducer';

export const AuthContext = createContext({});

const NovelContextProvider = (props: any): any => {
    const [jwtToken, setJwtToken] = useState('');
    const [state, dispatch] = useReducer(authReducer, jwtToken);

    useEffect(() => {
        localStorage.setItem('jwtToken', jwtToken);
    });

    return (
        <AuthContext.Provider value={{ state, jwtToken, setJwtToken, dispatch }}>{props.children}</AuthContext.Provider>
    );
};

export default NovelContextProvider;
