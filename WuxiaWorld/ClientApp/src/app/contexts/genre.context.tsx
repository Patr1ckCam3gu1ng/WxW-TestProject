import React, { createContext, useReducer } from 'react';

import { genreReducer } from '../reducers/genreReducer';
import { Inputbox } from '../models/inputbox';

export const GenreContext = createContext({});

const GenreContextProvider = (props: any): any => {
    const [value, dispatch] = useReducer(genreReducer, {
        value: '',
    } as Inputbox);

    return <GenreContext.Provider value={{ value, dispatch }}>{props.children}</GenreContext.Provider>;
};

export default GenreContextProvider;
