export interface Inputbox {
    value: string;
}

export class Commands {
    static GenreList = 'Genre_List';
    static GenreAssign = 'Genre_Assign';
    static GenreRemove = 'Genre_Remove';
    static GenreCreate = 'Genre_Create';
    static NovelList = 'Novel_List';
    static NovelCreate = 'Novel_Create';
    static ChapterCreate = 'Chapter_Create';
    static ChapterPublish = 'Chapter_Publish';
    static Login = 'Login';
    static Logout = 'Logout';
}
