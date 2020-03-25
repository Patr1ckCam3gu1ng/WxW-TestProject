import { Commands, Inputbox } from '../models/inputbox';
import { Action } from '../models/action.interface';
import genreService from '../services/genre.service';

export const GenreReducer = (state: Inputbox, action: Action) => {
    const jwtToken = localStorage.getItem('jwtToken')?.toString() || '';
    if (jwtToken === '') {
        action.print('User not yet authenticated. Please login');
        return state;
    }
    switch (action.type) {
        case Commands.GenreList: {
            return genreService.list(jwtToken, action, state);
        }
        case Commands.GenreCreate: {
            return genreService.create(action, jwtToken, state);
        }
    }

    return state;
};
