import { Commands } from '../models/inputbox';
import { Action } from '../models/action.interface';
import chapterService from '../services/chapter.service';
import authService from '../services/auth.service';
import { Chapter } from '../models/chapter.interface';

export const chapterReducer = (state: Chapter, action: Action) => {
    const jwtToken = authService.jwtToken(action);
    if (jwtToken !== '') {
        switch (action.type) {
            case Commands.ChapterCreate: {
                chapterService.create(jwtToken, action);
                return state;
            }
        }
    }
    return state;
};
