import chapterService from '../services/chapter.service';
import authService from '../services/auth.service';
import { Action } from '../models/action.interface';
import { Chapter } from '../models/chapter.interface';
import { Commands } from '../models/inputbox';

export const chapterReducer = (state: Chapter, action: Action) => {
    const jwtToken = authService.jwtToken(action);
    if (jwtToken !== '') {
        switch (action.type) {
            case Commands.ChapterCreate: {
                chapterService.create(jwtToken, action);
                return state;
            }
            case Commands.ChapterPublish: {
                chapterService.publish(jwtToken, action);
                return state;
            }
        }
    }
    return state;
};
