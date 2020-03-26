import apis from '../api';
import helper from './splitString.service';
import { Novel } from '../models/novel.interface';
import { Action } from '../models/action.interface';
import errorHelper from '../services/throwError.service';
import syntaxError from './syntaxError.service';

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
                    if (novel.trim() === '') {
                        return novel;
                    }
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
                            .catch(errorHelper.errorCode(action));
                        return;
                    }
                }
            }
        }
        syntaxError.print(action, 'create novels {1_novel_name} {2_novel_name}');
    },
};
