import { Action } from '../models/action.interface';
import helper from './splitString.service';
import apis from '../api';
import { ApiError } from '../models/apiError.interface';
import { ErrorMessage } from './throwError.service';
import { Chapter } from '../models/chapter.interface';

export default {
    create: (jwtToken: string, action: Action): void => {
        if (Array.isArray(action.inputValue) === true) {
            const splitChapterInputs = helper.splitQuoteString(action.inputValue) as string[];
            if (splitChapterInputs.length === 4) {
                const chapterNumber = splitChapterInputs[0];
                const chapterTitle = splitChapterInputs[1];
                const chapterContent = splitChapterInputs[2];
                const novelId = splitChapterInputs[3];

                if (!isNaN(chapterNumber as any) && !isNaN(novelId as any)) {
                    apis.post()
                        .novelChapter(jwtToken, {
                            number: Number(chapterNumber),
                            name: chapterTitle,
                            content: chapterContent,
                            novelId: Number(novelId),
                        } as Chapter)
                        .then((data: Chapter) => {
                            if (data.id !== 0) {
                                action.print('Success: Chapter successfully created for this novel');
                            }
                        })
                        .catch(function(error: ApiError) {
                            if (error.code === 401) {
                                action.print(ErrorMessage.AdminRole);
                            }
                            if (error.code === 400) {
                                action.print(error.message);
                            }
                        });

                    return;
                }
            }
        }
        action.runCommand('clear');
        setTimeout(() => {
            action.print('Error: Invalid syntax. Syntax should be:');
            action.print('create-chapter {chapterNumber} {chapterTitle} {chapterContent} {novelId}');
        }, 100);
        return;
    },
};
