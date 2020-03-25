import authService from '../services/auth.service';
import novelService from '../services/novel.service';
import { Novel } from '../models/novel.interface';
import { Commands } from '../models/inputbox';
import { Action } from '../models/action.interface';
export const novelReducer = (state: Novel, action: Action) => {
    const jwtToken = authService.jwtToken(action);
    if (jwtToken !== '') {
        switch (action.type) {
            case Commands.NovelList: {
                novelService.list(jwtToken, action);
                return state;
            }
            case Commands.NovelCreate: {
                novelService.create(action, jwtToken);
                return state;
            }
        }
    }

    return state;
};
