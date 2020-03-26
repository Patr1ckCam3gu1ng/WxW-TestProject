import errorHelper from './throwError.service';
import helper from './splitString.service';
import apis from '../api';
import syntaxError from './syntaxError.service';

import { GenreNovel } from '../models/genreNovel.interface';
import { Genre } from '../models/genre.interface';
import { Inputbox } from '../models/inputbox';
import { Action } from '../models/action.interface';
import { Novel } from '../models/novel.interface';

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
    list: (action: Action, jwtToken: string, state: Inputbox): Inputbox => {
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
    byNovelId: (action: Action, jwtToken: string, state: Inputbox): Inputbox => {
        if (Array.isArray(action.inputValue) === true) {
            const splitInputs = helper.splitQuoteString(action.inputValue) as string[];
            if (splitInputs.length > 0) {
                if (splitInputs.length === 3) {
                    if (splitInputs[0] === 'genres' && splitInputs[2] === 'novels') {
                        const genreId = splitInputs[1];
                        if (!isNaN(genreId as any)) {
                            apis.get()
                                .genreByNovelId(jwtToken, Number(genreId))
                                .then((genres: Novel[]) => {
                                    if (genres.length === 0) {
                                        action.print(
                                            'Error: No novels that belongs to the genre were found in the database',
                                        );
                                        return;
                                    }
                                    action.runCommand('clear');
                                    action.print('List of Novels that belong to the genre:');
                                    genres.map(genre => {
                                        action.print(`Id:        ${genre.id}`);
                                        action.print(`Name:      ${genre.name}`);
                                        action.print('');
                                        return genre;
                                    });
                                    return state;
                                })
                                .catch(errorHelper.errorCode(action));
                            return state;
                        }
                    }
                }
            }
        }
        syntaxError.print(action, 'list genres {genreId} novels');
        return state;
    },
    create: (action: Action, jwtToken: string, state: Inputbox): Inputbox => {
        if (Array.isArray(action.inputValue) === true) {
            const splitGenres = helper.splitQuoteString(action.inputValue) as string[];
            if (splitGenres.length > 0) {
                const genreList: Genre[] = [];
                splitGenres.map(genre => {
                    if (genre.trim() === '') {
                        return genre;
                    }
                    genreList.push({
                        name: genre,
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
                            .catch(errorHelper.errorCode(action));
                        return state;
                    }
                }
            }
        }
        const nameGenreText = 'name_of_genre';
        syntaxError.print(action, `create genres {1_${nameGenreText}} {2_${nameGenreText}} {3_${nameGenreText}}`);
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
                        .catch(errorHelper.errorCode(action));
                    return state;
                }
            }
        }
        action.print('Error: Incorrect input was provided');
        return state;
    },
};
