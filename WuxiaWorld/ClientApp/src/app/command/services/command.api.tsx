import apis from '../../api';
import { Genres } from '../models/genres.interface';
import { UserAccount } from '../models/userAccount.interface';

function throwError({ error }: { error: any }): object {
    const { status, data } = error.response;
    throw Object.assign({
        code: status,
        message: data,
    });
}

export default {
    get: {
        genres: (commandType: string, authenticationHeader: string): Promise<Genres[] | any> => {
            return apis
                .get()
                .commands(authenticationHeader, commandType)
                .then((value: Genres[]) => {
                    return value;
                })
                .catch(function(error) {
                    throwError({ error: error });
                });
        },
    },
    login: (userAccount: UserAccount): Promise<string | void> => {
        return apis
            .auth()
            .login(userAccount)
            .then((value: string) => {
                return value;
            })
            .catch(function(error) {
                console.log(error.response.data); // this is the part you need that catches 400 request
            });
    },
};
