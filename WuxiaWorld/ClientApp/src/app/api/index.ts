import axios from 'axios';
import common from '../common';
import { UserAccount } from '../models/userAccount.interface';
import { Genre } from '../models/genre.interface';
import { Novel } from '../models/novel.interface';

const apiRootUrl = common.apiUrl();

export default {
    get(): {
        genre: (authenticationHeader: string, commandType: string) => Promise<Genre[]>;
        novel: (authenticationHeader: string, commandType: string) => Promise<Novel[]>;
    } {
        return {
            genre: (authenticationHeader: string, commandType: string): Promise<Genre[]> => {
                return (async function(): Promise<Genre[]> {
                    return (
                        await axios.get(`${apiRootUrl}/${commandType}`, {
                            headers: { Authorization: authenticationHeader },
                        })
                    ).data;
                })().then(data => data);
            },
            novel: (authenticationHeader: string, commandType: string): Promise<Novel[]> => {
                return (async function (): Promise<Novel[]> {
                    return (
                        await axios.get(`${apiRootUrl}/${commandType}`, {
                            headers: { Authorization: authenticationHeader },
                        })
                    ).data;
                })().then(data => data);
            },
        };
    },
    auth(): { login: (userAccount: UserAccount) => Promise<string> } {
        return {
            login: (userAccount: UserAccount): Promise<string> => {
                return (async function(): Promise<string> {
                    return (await axios.post(`${apiRootUrl}/login`, userAccount)).data;
                })().then(data => data);
            },
        };
    },
};
