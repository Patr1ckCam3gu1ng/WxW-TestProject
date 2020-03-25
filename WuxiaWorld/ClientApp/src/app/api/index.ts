import axios from 'axios';
import common from '../common';
import { UserAccount } from '../models/userAccount.interface';
import { Genre } from '../models/genre.interface';
import helper from '../services/throwError.service';
import { Novel } from '../models/novel.interface';
import { GenreNovel } from '../models/genreNovel.interface';
import { ApiError } from '../models/apiError.interface';

const apiRootUrl = common.apiUrl();

export default {
    get(): {
        genre: (authenticationHeader: string) => Promise<Genre[]>;
        novels: (authenticationHeader: string) => Promise<Novel[]>;
    } {
        return {
            genre: (authenticationHeader: string): Promise<Genre[]> => {
                return (async function(): Promise<Genre[]> {
                    return await axios
                        .get(`${apiRootUrl}/genres`, {
                            headers: { Authorization: authenticationHeader },
                        })
                        .then(value => {
                            return value.data;
                        });
                })();
            },
            novels: (authenticationHeader: string): Promise<Novel[]> => {
                return (async function(): Promise<Novel[]> {
                    return await axios
                        .get(`${apiRootUrl}/novels`, {
                            headers: { Authorization: authenticationHeader },
                        })
                        .then(value => {
                            return value.data;
                        });
                })();
            },
        };
    },
    post(): {
        genre: (authenticationHeader: string, genreList: Genre[]) => Promise<Genre[]>;
        novels: (authenticationHeader: string, genreList: Novel[]) => Promise<Novel[]>;
        novelGenre: (authenticationHeader: string, genreNovel: GenreNovel) => Promise<number | ApiError>;
    } {
        return {
            genre: (authenticationHeader: string, genreList: Genre[]): Promise<Genre[]> => {
                return (async function(): Promise<Genre[]> {
                    return await axios
                        .post(`${apiRootUrl}/genres`, genreList, {
                            headers: { Authorization: authenticationHeader },
                        })
                        .then(value => value.data)
                        .catch(error => helper.throwError(error));
                })();
            },
            novels: (authenticationHeader: string, genreList: Novel[]): Promise<Novel[]> => {
                return (async function(): Promise<Novel[]> {
                    return await axios
                        .post(`${apiRootUrl}/novels`, genreList, {
                            headers: { Authorization: authenticationHeader },
                        })
                        .then(value => value.data)
                        .catch(error => helper.throwError(error));
                })();
            },
            novelGenre: (authenticationHeader: string, genreNovel: GenreNovel): Promise<number | ApiError> => {
                return (async function(): Promise<number | ApiError> {
                    return await axios
                        .post(`${apiRootUrl}/novels/${genreNovel.novelId}/genre/${genreNovel.genreId}`, genreNovel, {
                            headers: { Authorization: authenticationHeader },
                        })
                        .then(response => response.status)
                        .catch(error => helper.throwError(error));
                })();
            },
        };
    },
    auth(): { login: (userAccount: UserAccount) => Promise<any> } {
        return {
            login: (userAccount: UserAccount): Promise<any> => {
                return (async function(): Promise<any> {
                    return await axios
                        .post(`${apiRootUrl}/login`, userAccount)
                        .then(data => data)
                        .catch(error => helper.throwError(error));
                })();
            },
        };
    },
};
