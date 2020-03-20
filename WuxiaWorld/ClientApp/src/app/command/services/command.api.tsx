import apis from '../../api';
import { Genre } from '../models/genre.interface';
import { UserAccount } from '../models/userAccount.interface';
import { Novel } from '../models/novel.interface';

function throwError({ error }: { error: any }): object {
    const { status, data } = error.response;
    throw Object.assign({
        code: status,
        message: data,
    });
}

export default {
    get: {
        genres: (commandType: string, authenticationHeader: string): Promise<Genre[] | any> => {
            return apis
                .get()
                .genre(authenticationHeader, commandType)
                .then((value: Genre[]) => {
                    return value;
                })
                .catch(function(error) {
                    throwError({ error: error });
                });
        },
        novels: (commandType: string, authenticationHeader: string): Promise<Novel[] | any> => {
            return apis
                .get()
                .novel(authenticationHeader, commandType)
                .then((value: Novel[]) => {
                    return value;
                })
                .catch(function(error) {
                    throwError({ error: error });
                });
        },
    },
    login: (userAccount: UserAccount): Promise<string | any> => {
        return apis
            .auth()
            .login(userAccount)
            .then((value: string) => {
                return value;
            })
            .catch(function(error) {
                throwError({ error: error });
            });
    },
};
