import apis from '../../api';
import actions from './command.action';
import { Genres } from '../models/genres.interface';

export default {
    get: (commandType: string): Promise<Genres[] | void> => {
        return apis
            .get()
            .commands(actions.authenticationHeader(), commandType)
            .then((value: Genres[]) => {
                return value;
            })
            .catch(function(error) {
                console.log(error.response.data); // this is the part you need that catches 400 request
            });
    },
};
