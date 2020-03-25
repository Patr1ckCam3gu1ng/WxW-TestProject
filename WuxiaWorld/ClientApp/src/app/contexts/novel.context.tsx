import React, { createContext, useReducer } from 'react';

import { Novel } from '../models/novel.interface';
import { novelReducer } from '../reducers/novelReducer';

export const NovelContext = createContext({});

const NovelContextProvider = (props: any): any => {
    const [value, dispatch] = useReducer(novelReducer, {} as Novel);

    return <NovelContext.Provider value={{ value, dispatch }}>{props.children}</NovelContext.Provider>;
};

export default NovelContextProvider;
