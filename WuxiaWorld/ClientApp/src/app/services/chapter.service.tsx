import apis from '../api';

import helper from './splitString.service';
import syntaxError from './syntaxError.service';
import errorHelper from '../services/throwError.service';

import { ChapterPublish } from '../models/chapterPublish.interface';
import { Action } from '../models/action.interface';
import { Chapter } from '../models/chapter.interface';

function getApiChapterByNovelId(
    jwtToken: string,
    novelId: string,
    chapterNumber: string,
    action: Action,
    isIncludeContent: boolean,
) {
    apis.get()
        .chapterByNovelId(jwtToken, Number(novelId), Number(chapterNumber), isIncludeContent)
        .then((chapters: Chapter[]) => {
            if (chapters.length === 0) {
                action.runCommand('clear');
                action.print('No published chapter for this novel in the database ');
                return;
            }
            action.runCommand('clear');

            const NovelWithoutContent = (): any => {
                action.print("Novel's Chapters:");
                chapters.map(chapter => {
                    action.print(`      Chapter Number: ${chapter.number}`);
                    action.print(`      Chapter Title:  ${chapter.name}`);
                    action.print(`      Published on:   ${chapter.chapterPublishDate}`);
                    action.print('');
                    return chapter;
                });
            };
            const NovelWithContent = (): any => {
                action.print("Chapter's Content:");
                chapters.map(chapter => {
                    action.print(`      Chapter Title:  ${chapter.name}`);
                    action.print(`      Content:  ${chapter.content}`);
                    action.print('');
                    return chapter;
                });
            };

            if (isIncludeContent) {
                NovelWithContent();
            } else {
                NovelWithoutContent();
            }
        })
        .catch(errorHelper.errorCode(action));
}

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
                        .catch(errorHelper.errorCode(action));

                    return;
                }
            }
        }
        syntaxError.print(action, 'create-chapter {chapterNumber} {chapterTitle} {chapterContent} {novelId}');
        return;
    },
    publish: (jwtToken: string, action: Action): void => {
        if (Array.isArray(action.inputValue) === true) {
            const splitChapterPublishInputs = helper.splitQuoteString(action.inputValue) as string[];
            if (splitChapterPublishInputs.length === 4) {
                const chapterNumber = splitChapterPublishInputs[3];
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
                        .catch(errorHelper.errorCode(action));
                    return;
                }
            }
        }
        syntaxError.print(action, 'publish novels {novelId} chapters {chapterNumber}');
        return;
    },
    chapterByNovelId: (jwtToken: string, action: Action): void => {
        if (Array.isArray(action.inputValue) === true) {
            const chapterNovel = helper.splitQuoteString(action.inputValue) as string[];
            if (chapterNovel.length === 3) {
                if (chapterNovel[0] === 'novels' && chapterNovel[2] === 'chapters') {
                    const novelId = chapterNovel[1];
                    if (!isNaN(novelId as any)) {
                        getApiChapterByNovelId(jwtToken, novelId, '0', action, false);
                        return;
                    }
                }
            }
        }
        syntaxError.print(action, 'list novels {novelId} chapters');
        return;
    },
    chapterContents: (jwtToken: string, action: Action): void => {
        if (Array.isArray(action.inputValue) === true) {
            const chapterNovel = helper.splitQuoteString(action.inputValue) as string[];
            if (chapterNovel.length === 4) {
                if (chapterNovel[0] === 'novels' && chapterNovel[2] === 'chapter') {
                    const novelId = chapterNovel[1];
                    const chapterId = chapterNovel[3];
                    if (!isNaN(novelId as any)) {
                        getApiChapterByNovelId(jwtToken, novelId, chapterId, action, true);
                        return;
                    }
                }
            }
        }
        syntaxError.print(action, 'chapter-content novels {novelId}');
        return;
    },
};
