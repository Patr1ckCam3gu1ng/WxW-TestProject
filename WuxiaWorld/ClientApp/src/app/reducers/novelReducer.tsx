import { Commands } from '../models/inputbox';
import { Action } from '../models/action.interface';
import { Novel } from '../models/novel.interface';
import authService from '../services/auth.service';
import novelService from '../services/novel.service';

export const novelReducer = (state: Novel, action: Action) => {
    const jwtToken = authService.jwtToken(action);
    if (jwtToken !== '') {
        console.log('wwww')
        switch (action.type) {
            case Commands.NovelList: {
                novelService.list(jwtToken, action);
                break;
            }
        }
    }

    return state;
};
