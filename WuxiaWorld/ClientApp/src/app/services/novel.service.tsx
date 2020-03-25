import { Action } from '../models/action.interface';
import apis from '../api';
import { Novel } from '../models/novel.interface';

export default {
    list: (jwtToken: string, action: Action): void => {
        apis.get()
            .novels(jwtToken)
            .then((novels: Novel[]) => {
                action.runCommand('clear');
                action.print('List of published Novels:');
                novels.map(novel => {
                    action.print(`      Id: ${novel.id}`);
                    action.print(`      Name: ${novel.name}`);
                    action.print('      ---');
                    return novel;
                });
            });
        return;
    },
};
