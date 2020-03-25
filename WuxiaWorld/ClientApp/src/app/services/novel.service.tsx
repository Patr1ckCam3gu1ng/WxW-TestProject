import { Action } from '../models/action.interface';
import apis from '../api';
import { Novel } from '../models/novel.interface';
import helper from './splitString.service';
import { ApiError } from '../models/apiError.interface';
import { ErrorMessage } from './throwError.service';

export default {
    list: (jwtToken: string, action: Action): void => {
        apis.get()
            .novels(jwtToken)
            .then((novels: Novel[]) => {
                action.runCommand('clear');
                action.print('List of Published Novels:');
                novels.map(novel => {
                    action.print(`      Id: ${novel.id}`);
                    action.print(`      Name: ${novel.name}`);
                    action.print('');
                    return novel;
                });
            });
        return;
    },
    create: (action: Action, jwtToken: string): void => {
        if (Array.isArray(action.inputValue) === true) {
            const splitNovel = helper.splitQuoteString(action.inputValue) as string[];
            if (splitNovel.length > 0) {
                const novelList: Novel[] = [];
                splitNovel.map(novel => {
                    novelList.push({
                        name: novel,
                    } as Novel);
                    return novel;
                });
                if (novelList.length > 0) {
                    if (novelList.length > 0) {
                        apis.post()
                            .novels(jwtToken, novelList)
                            .then((novel: Novel[]) => {
                                action.print('Ok: Novel added to the database ');
                                return novel;
                            })
                            .catch(function(error: ApiError) {
                                if (error.code === 401) {
                                    action.print(ErrorMessage.AdminRole);
                                }
                            });
                        return;
                    }
                }
            }
        }
        action.print('Error: Please enter the correct command to create a novel');
    },
};
