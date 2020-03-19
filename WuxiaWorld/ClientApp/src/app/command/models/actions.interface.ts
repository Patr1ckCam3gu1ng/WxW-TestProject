import { Genres } from './genres.interface';

export interface Actions {
    setStateListGenres(response: Genres[]): void;
    setInputValue(value: string): void;
    setStateEmpty(): void;
    setMessage(value: string): void;
}
