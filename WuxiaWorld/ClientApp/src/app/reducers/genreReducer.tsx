import { Commands, Inputbox } from '../models/inputbox';
import { Action } from '../models/action.interface';
import genreService from '../services/genre.service';
import authService from '../services/auth.service';

export const genreReducer = (state: Inputbox, action: Action) => {
    const jwtToken = authService.jwtToken(action);
    if (jwtToken !== '') {
        switch (action.type) {
            case Commands.GenreList: {
                return genreService.list(jwtToken, action, state);
            }
            case Commands.GenreCreate: {
                return genreService.create(action, jwtToken, state);
            }
            case Commands.GenreAssign: {
                return genreService.assign(action, jwtToken, state, null);
            }
            case Commands.GenreRemove: {
                return genreService.assign(action, jwtToken, state, true);
            }
        }
    }

    return state;
};
