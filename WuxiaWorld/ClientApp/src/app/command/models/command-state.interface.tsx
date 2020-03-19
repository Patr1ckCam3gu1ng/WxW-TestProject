import { Genres } from './genres.interface';

export interface CommandState {
    genres: Genres[];
    inputValue: string;
    message: string;
}
