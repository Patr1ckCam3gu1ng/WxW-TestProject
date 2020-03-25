import helper from './splitString.service';
import apis from '../api';
import { ErrorMessage } from './throwError.service';
import { ChapterPublish } from '../models/chapterPublish.interface';
import { Action } from '../models/action.interface';
import { Chapter } from '../models/chapter.interface';
import { ApiError } from '../models/apiError.interface';

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
            action.print(ErrorMessage.InvalidSyntax);
            action.print('create-chapter {chapterNumber} {chapterTitle} {chapterContent} {novelId}');
        }, 100);
        return;
    },
    publish: (jwtToken: string, action: Action): void => {
        if (Array.isArray(action.inputValue) === true) {
            const splitChapterPublishInputs = helper.splitQuoteString(action.inputValue) as string[];
            if (splitChapterPublishInputs.length === 2) {
                const chapterNumber = splitChapterPublishInputs[0];
                const novelId = splitChapterPublishInputs[1];
                if (!isNaN(chapterNumber as any) && !isNaN(novelId as any)) {
                    apis.post()
                        .novelChapterPublish(jwtToken, {
                            number: Number(chapterNumber),
                            novelId: Number(novelId),
                        } as ChapterPublish)
                        .then((data: Chapter) => {
                            if (data.id !== 0) {
                                action.print('Success: Chapter successfully published');
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
            action.print(ErrorMessage.InvalidSyntax);
            action.print('publish-chapter {chapterNumber} {novelId}');
        }, 100);
        return;
    },
};
