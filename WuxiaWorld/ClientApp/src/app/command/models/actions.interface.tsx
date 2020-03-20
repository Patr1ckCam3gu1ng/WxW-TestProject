import { Genre } from './genre.interface';
import { Novel } from './novel.interface';

export interface Actions {
    setStateListGenres(response: Genre[]): void;
    setStateListNovels(response: Novel[]): void;
    setInputValue(value: string): void;
    setStateEmpty(): Actions;
    setMessage(value: string): void;
}
