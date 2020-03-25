import React, { createContext, useReducer } from 'react';
import { chapterReducer } from '../reducers/chapter.reducer';
import { Chapter } from '../models/chapter.interface';

export const ChapterContext = createContext({});

const ChapterContextProvider = (props: any): any => {
    const [value, dispatch] = useReducer(chapterReducer, {} as Chapter);

    return <ChapterContext.Provider value={{ value, dispatch }}>{props.children}</ChapterContext.Provider>;
};

export default ChapterContextProvider;
