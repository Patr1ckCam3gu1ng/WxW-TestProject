import React, { createContext, useReducer } from 'react';

import { GenreReducer } from '../reducers/genre.reducer';
import { Inputbox } from '../models/inputbox';

export const GenreContext = createContext({});

const GenreContextProvider = (props: any): any => {
    const [value, dispatch] = useReducer(GenreReducer, {
        value: '',
    } as Inputbox);

    return <GenreContext.Provider value={{ value, dispatch }}>{props.children}</GenreContext.Provider>;
};

export default GenreContextProvider;
