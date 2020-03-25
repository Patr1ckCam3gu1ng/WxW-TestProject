import axios from 'axios';
import common from '../common';
import { UserAccount } from '../models/userAccount.interface';
import { Genre } from '../models/genre.interface';
import helper from '../services/throwError.service';

const apiRootUrl = common.apiUrl();

export default {
    get(): {
        genre: (authenticationHeader: string) => Promise<Genre[]>;
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
        };
    },
    post(): {
        genre: (authenticationHeader: string, genreList: Genre[]) => Promise<Genre[]>;
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
