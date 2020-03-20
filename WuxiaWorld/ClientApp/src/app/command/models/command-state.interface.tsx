import { Genre } from './genre.interface';
import { Novel } from './novel.interface';

export interface CommandState {
    genres: Genre[];
    novels: Novel[];
    inputValue: string;
    message: string;
}
