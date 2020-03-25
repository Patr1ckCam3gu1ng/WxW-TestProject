import { Action } from '../models/action.interface';
import { Inputbox } from '../models/inputbox';
import helper from './splitString.service';
import { Genre } from '../models/genre.interface';
import apis from '../api';
import { ApiError } from '../models/apiError.interface';

export default {
    list: (jwtToken: string, action: Action, state: Inputbox): Inputbox => {
        apis.get()
            .genre(jwtToken)
            .then((genres: Genre[]) => {
                action.runCommand('clear');
                action.print('List of Genres:');
                genres.map(genre => {
                    action.print(`      ${genre.name}`);
                    return genre;
                });
                return state;
            });

        return state;
    },
    create: (action: Action, jwtToken: string, state: Inputbox): Inputbox => {
        if (Array.isArray(action.inputValue) === true) {
            const splitGenre = helper.splitQuoteString(action.inputValue) as string[];
            if (splitGenre.length > 0) {
                const genreList: Genre[] = [];
                splitGenre.map(genre => {
                    genreList.push({
                        name: genre.replace(/"/g, ''),
                    } as Genre);
                    return genre;
                });
                if (genreList.length > 0) {
                    if (genreList.length > 0) {
                        apis.post()
                            .genre(jwtToken, genreList)
                            .then((genres: Genre[]) => {
                                action.print('Ok: Genre added to the database ');
                                console.log(genres);
                                return genres;
                            })
                            .catch(function(error: ApiError) {
                                if (error.code === 401) {
                                    action.print('Error: The admin user is the only one who can write to the repo.');
                                }
                            });
                        return state;
                    }
                }
            }
        }
        action.print('Error: Please enter the correct command to create a genre');
        return state;
    },
};
