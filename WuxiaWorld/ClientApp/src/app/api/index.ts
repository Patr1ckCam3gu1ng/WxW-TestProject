import axios from 'axios';
import common from '../common';
import { UserAccount } from '../models/userAccount.interface';
import { Genre } from '../models/genre.interface';
import helper from '../services/throwError.service';

const apiRootUrl = common.apiUrl();

export default {
    get(): {
        genre: (authenticationHeader: string) => Promise<Genre[]>;
        // novel: (authenticationHeader: string, commandType: string) => Promise<Novel[]>;
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
            // novel: (authenticationHeader: string, commandType: string): Promise<Novel[]> => {
            //     return (async function (): Promise<Novel[]> {
            //         return (
            //             await axios.get(`${apiRootUrl}/${commandType}`, {
            //                 headers: { Authorization: authenticationHeader },
            //             })
            //         ).data;
            //     })().then(data => data);
            // },
        };
    },
    auth(): { login: (userAccount: UserAccount) => Promise<any> } {
        return {
            login: (userAccount: UserAccount): Promise<any> => {
                return (async function(): Promise<any> {
                    return await axios
                        .post(`${apiRootUrl}/login`, userAccount)
                        .then(data => data)
                        .catch(error => {
                            helper.throwError(error);
                        });
                })();
            },
        };
    },
};
