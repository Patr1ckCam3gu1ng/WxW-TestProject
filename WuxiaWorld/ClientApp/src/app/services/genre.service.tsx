import { ErrorMessage } from './throwError.service';
import helper from './splitString.service';

import { ApiError } from '../models/apiError.interface';
import { GenreNovel } from '../models/genreNovel.interface';
import { Action } from '../models/action.interface';
import { Inputbox } from '../models/inputbox';
import { Genre } from '../models/genre.interface';

import apis from '../api';

function getNovelGenreIds(splitNovelGenre: string[]): GenreNovel {
    let novelId: number | null = null;
    let genreId: number | null = null;
    splitNovelGenre.map(value => {
        const code = value.replace(/"/g, '');
        if (code.indexOf('novel') > -1) {
            novelId = Number(code.split(' ')[1]);
        }
        if (code.indexOf('genre') > -1) {
            genreId = Number(code.split(' ')[1]);
        }
        return value;
    });
    return {
        novelId,
        genreId,
    } as GenreNovel;
}

export default {
    list: (jwtToken: string, action: Action, state: Inputbox): Inputbox => {
        apis.get()
            .genre(jwtToken)
            .then((genres: Genre[]) => {
                action.runCommand('clear');
                action.print('List of Genres:');
                genres.map(genre => {
                    action.print(`Id:        ${genre.id}`);
                    action.print(`Name:      ${genre.name}`);
                    action.print('');
                    return genre;
                });
                return state;
            });

        return state;
    },
    create: (action: Action, jwtToken: string, state: Inputbox): Inputbox => {
        if (Array.isArray(action.inputValue) === true) {
            const splitGenres = helper.splitQuoteString(action.inputValue) as string[];
            if (splitGenres.length > 0) {
                const genreList: Genre[] = [];
                splitGenres.map(genre => {
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
                                return genres;
                            })
                            .catch(function(error: ApiError) {
                                if (error.code === 401) {
                                    action.print(ErrorMessage.AdminRole);
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
    assign: (action: Action, jwtToken: string, state: Inputbox, isUnAssign: boolean | null): Inputbox => {
        if (Array.isArray(action.inputValue) === true) {
            const splitNovelGenre = helper.splitQuoteString(action.inputValue) as string[];
            if (splitNovelGenre.length > 0) {
                const novelGenreIds = getNovelGenreIds(splitNovelGenre);
                if (novelGenreIds.novelId !== null && novelGenreIds.genreId !== null) {
                    apis.post()
                        .novelGenre(jwtToken, novelGenreIds, isUnAssign)
                        .then(status => {
                            if (status === 200) {
                                if (isUnAssign === true) {
                                    action.print('Success: Genre was unassigned/removed from the Novel');
                                    return state;
                                }
                                action.print('Success: Genre assigned to Novel');
                            }
                        })
                        .catch(function(error: ApiError) {
                            if (error.code === 401) {
                                action.print(ErrorMessage.AdminRole);
                            }
                            if (error.code === 400) {
                                action.print(error.message);
                            }
                        });
                    return state;
                }
            }
        }
        action.print('Error: Incorrect input was provided');
        return state;
    },
};
