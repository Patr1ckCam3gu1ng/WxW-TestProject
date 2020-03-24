import { Commands, Inputbox } from '../models/inputbox';
import { Action } from '../models/action.interface';
import apis from '../api';
import { Genre } from '../models/genre.interface';

export const GenreReducer = (state: Inputbox, action: Action) => {
    switch (action.type) {
        case Commands.SendCommand: {
            const jwtToken = localStorage.getItem('jwtToken')?.toString();
            if (typeof jwtToken !== 'undefined') {
                if (jwtToken !== '') {
                    apis.get()
                        .genre(jwtToken)
                        .then((genres: Genre[]) => {
                            action.print('List of Genres:');
                            genres.map(genre => {
                                action.print(genre.name);
                            });
                            return genres;
                        });
                    return state;
                }
            }
            action.print('User not yet authenticated. Please login');
        }
    }

    return state;
};
