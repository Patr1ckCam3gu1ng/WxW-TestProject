import axios from 'axios';
import common from '../common';
import { Novel } from '../models/novel.interface';
import { GenreNovel } from '../models/genreNovel.interface';
import { Chapter } from '../models/chapter.interface';
import { Genre } from '../models/genre.interface';
import { ApiError } from '../models/apiError.interface';
import { UserAccount } from '../models/userAccount.interface';
import helper from '../services/throwError.service';
import { ChapterPublish } from '../models/chapterPublish.interface';

const apiRootUrl = common.apiUrl();

export default {
    get(): {
        genre: (authenticationHeader: string) => Promise<Genre[]>;
        novels: (authenticationHeader: string) => Promise<Novel[]>;
    } {
        return {
            genre: (authenticationHeader: string): Promise<Genre[]> => {
                return (async function(): Promise<Genre[]> {
                    return await axios
                        .get(`${apiRootUrl}/genres`, {
                            headers: { Authorization: authenticationHeader },
                        })
                        .then(value => {
                            return value.data;
                        });
                })();
            },
            novels: (authenticationHeader: string): Promise<Novel[]> => {
                return (async function(): Promise<Novel[]> {
                    return await axios
                        .get(`${apiRootUrl}/novels`, {
                            headers: { Authorization: authenticationHeader },
                        })
                        .then(value => {
                            return value.data;
                        });
                })();
            },
        };
    },
    post(): {
        genre: (authenticationHeader: string, genreList: Genre[]) => Promise<Genre[]>;
        novelGenre: (
            authenticationHeader: string,
            genreNovel: GenreNovel,
            isUnAssign: boolean | null,
        ) => Promise<number | ApiError>;
        novelChapter: (authenticationHeader: string, chapter: Chapter) => Promise<Chapter>;
        novelChapterPublish: (authenticationHeader: string, chapterPublish: ChapterPublish) => Promise<Chapter>;
        novels: (authenticationHeader: string, genreList: Novel[]) => Promise<Novel[]>;
    } {
        return {
            genre: (authenticationHeader: string, genreList: Genre[]): Promise<Genre[]> => {
                return (async function(): Promise<Genre[]> {
                    return await axios
                        .post(`${apiRootUrl}/genres`, genreList, {
                            headers: { Authorization: authenticationHeader },
                        })
                        .then(value => value.data)
                        .catch(error => helper.throwError(error));
                })();
            },
            novels: (authenticationHeader: string, genreList: Novel[]): Promise<Novel[]> => {
                return (async function(): Promise<Novel[]> {
                    return await axios
                        .post(`${apiRootUrl}/novels`, genreList, {
                            headers: { Authorization: authenticationHeader },
                        })
                        .then(value => value.data)
                        .catch(error => helper.throwError(error));
                })();
            },
            novelGenre: (
                authenticationHeader: string,
                genreNovel: GenreNovel,
                isUnAssign: boolean | null,
            ): Promise<number | ApiError> => {
                return (async function(): Promise<number | ApiError> {
                    console.log(isUnAssign);
                    const unAssign = isUnAssign === null ? '' : '/unAssign';
                    return await axios
                        .post(
                            `${apiRootUrl}/novels/${genreNovel.novelId}/genre/${genreNovel.genreId}${unAssign}`,
                            genreNovel,
                            {
                                headers: { Authorization: authenticationHeader },
                            },
                        )
                        .then(response => response.status)
                        .catch(error => helper.throwError(error));
                })();
            },
            novelChapter: (authenticationHeader: string, chapter: Chapter): Promise<Chapter> => {
                return (async function(): Promise<Chapter> {
                    return await axios
                        .post(`${apiRootUrl}/novels/${chapter.novelId}/chapters`, chapter, {
                            headers: { Authorization: authenticationHeader },
                        })
                        .then(value => value.data)
                        .catch(error => helper.throwError(error));
                })();
            },
            novelChapterPublish: (authenticationHeader: string, chapterPublish: ChapterPublish): Promise<Chapter> => {
                return (async function(): Promise<Chapter> {
                    return await axios
                        .post(
                            `${apiRootUrl}/novels/${chapterPublish.novelId}/chapters/${chapterPublish.number}/publish`,
                            null,
                            {
                                headers: { Authorization: authenticationHeader },
                            },
                        )
                        .then(value => value.data)
                        .catch(error => helper.throwError(error));
                })();
            },
        };
    },
    auth(): { login: (userAccount: UserAccount) => Promise<any> } {
        return {
            login: (userAccount: UserAccount): Promise<any> => {
                return (async function(): Promise<any> {
                    return await axios
                        .post(`${apiRootUrl}/login`, userAccount)
                        .then(data => data)
                        .catch(error => helper.throwError(error));
                })();
            },
        };
    },
};
