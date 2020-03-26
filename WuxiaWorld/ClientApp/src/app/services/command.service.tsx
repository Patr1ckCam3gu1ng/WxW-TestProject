import { Action, AuthAction } from '../models/action.interface';
import { TerminalCommands } from '../models/command.interface';
import { Commands } from '../models/inputbox';

export default function(
    genreDispatch: any,
    authDispatch: any,
    chapterDispatch: any,
    novelDispatch: any,
    setJwtToken: any,
) {
    return {
        'chapter-content': {
            method: (args: any, print: any, runCommand: any) => {
                chapterDispatch({
                    type: Commands.ChapterContent,
                    inputValue: args._,
                    print,
                    runCommand,
                } as Action);
                return;
            },
        },
        'assign-genre': {
            method: (args: any, print: any, runCommand: any) => {
                genreDispatch({
                    type: Commands.GenreAssign,
                    inputValue: args._,
                    print,
                    runCommand,
                } as Action);
                return;
            },
        },
        'unassign-genre': {
            method: (args: any, print: any, runCommand: any) => {
                genreDispatch({
                    type: Commands.GenreRemove,
                    inputValue: args._,
                    print,
                    runCommand,
                } as Action);
                return;
            },
        },
        'create-chapter': {
            method: (args: any, print: any, runCommand: any) => {
                chapterDispatch({
                    type: Commands.ChapterCreate,
                    inputValue: args._,
                    print,
                    runCommand,
                } as Action);
                return;
            },
        },
        publish: {
            method: (args: any, print: any, runCommand: any) => {
                chapterDispatch({
                    type: Commands.ChapterPublish,
                    inputValue: args._,
                    print,
                    runCommand,
                } as Action);
                return;
            },
        },
        list: {
            method: (args: any, print: any, runCommand: any) => {
                const inputValue = args._[0];
                if (inputValue === 'genres') {
                    const novelId = args._[1];
                    if (typeof novelId === 'undefined') {
                        genreDispatch({
                            type: Commands.GenreList,
                            inputValue: inputValue,
                            print,
                            runCommand,
                        } as Action);
                        return;
                    }
                }
                if (inputValue === 'genres') {
                    genreDispatch({
                        type: Commands.NovelsByGenreId,
                        inputValue: args._,
                        print,
                        runCommand,
                    } as Action);
                    return;
                }
                if (inputValue === 'novels') {
                    const novelId = args._[1];
                    if (typeof novelId === 'undefined') {
                        novelDispatch({
                            type: Commands.NovelList,
                            print,
                            runCommand,
                        } as Action);
                        return;
                    }
                }
                if (inputValue === 'novels') {
                    chapterDispatch({
                        type: Commands.ChaptersByNovelId,
                        inputValue: args._,
                        print,
                        runCommand,
                    } as Action);
                    return;
                } else {
                    print('Command not valid');
                }
            },
        },
        create: {
            method: (args: any, print: any, runCommand: any) => {
                // const inputValue = args._[0];
                if (args._[0] === 'genres') {
                    genreDispatch({
                        type: Commands.GenreCreate,
                        inputValue: args._.filter((value: string, index: number) => index > 0),
                        print,
                        runCommand,
                    } as Action);
                    return;
                }
                if (args._[0] === 'novels') {
                    novelDispatch({
                        type: Commands.NovelCreate,
                        inputValue: args._.filter((value: string, index: number) => index > 0),
                        print,
                        runCommand
                    } as Action);
                    return;
                } else {
                    print('Command not valid');
                }
            },
        },
        login: {
            method: (args: any, print: any) => {
                const inputs = args._;
                if (inputs.length === 2) {
                    authDispatch({
                        type: Commands.Login,
                        username: inputs[0],
                        password: inputs[1],
                        print,
                        setJwtToken,
                    } as AuthAction);
                } else {
                    print('Invalid input. Please provide username and password properly');
                }
            },
        },
        logout: {
            method: (args: any, print: any) => {
                authDispatch({
                    type: Commands.Logout,
                    print,
                } as AuthAction);
            },
        },
    } as TerminalCommands;
}
