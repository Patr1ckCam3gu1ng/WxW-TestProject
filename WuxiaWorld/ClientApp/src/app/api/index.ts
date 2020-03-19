import axios from 'axios';
import common from '../common';
import { UserAccount } from '../command/models/userAccount.interface';
import { Genres } from '../command/models/genres.interface';

const apiRootUrl = common.apiUrl();

export default {
    get(): { commands: (authenticationHeader: string, commandType: string) => Promise<Genres[]> } {
        return {
            commands: (authenticationHeader: string, commandType: string): Promise<Genres[]> => {
                return (async function(): Promise<Genres[]> {
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
                    return (await axios.post(`${apiRootUrl}/login`, userAccount).catch(data => data.response)).data;
                })().then(data => data);
            },
        };
    },
};
