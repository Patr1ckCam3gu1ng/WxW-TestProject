export interface Action {
    type: string;
    inputValue: string;
    print: any;
}

export interface AuthAction {
    type: string;
    username: string;
    password: string;
    print: any;
    setJwtToken: any;
    jwtToken: any;
}
