import React, { createContext, useReducer } from 'react';

import { InputBoxReducer } from '../reducers/inputBoxReducer';
import { InputboxInterface } from '../models/inputbox.interface';

export const NovelContext = createContext({});

const NovelContextProvider = (props: { children: React.ReactNode }): any => {
    const initialArg: InputboxInterface = {
        value: '',
    };
    const [value, dispatch] = useReducer(InputBoxReducer, initialArg);
    return <NovelContext.Provider value={{ value, dispatch }}>{props.children}</NovelContext.Provider>;
};

export default NovelContextProvider;
