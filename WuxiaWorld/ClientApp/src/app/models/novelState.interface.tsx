import { Genre } from './genre.interface';
import { Novel } from './novel.interface';

export interface NovelState {
    genres: Genre[];
    novels: Novel[];
    inputValue: string;
    message: string;
}
