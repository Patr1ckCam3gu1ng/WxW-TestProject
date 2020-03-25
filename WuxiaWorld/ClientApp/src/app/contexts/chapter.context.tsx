import React, { createContext, useReducer } from 'react';
import { Chapter } from '../models/chapter.interface';
import { chapterReducer } from '../reducers/chapter.reducer';

export const ChapterContext = createContext({});

const ChapterContextProvider = (props: any): any => {
    const [value, dispatch] = useReducer(chapterReducer, {} as Chapter);

    return <ChapterContext.Provider value={{ value, dispatch }}>{props.children}</ChapterContext.Provider>;
};

export default ChapterContextProvider;
