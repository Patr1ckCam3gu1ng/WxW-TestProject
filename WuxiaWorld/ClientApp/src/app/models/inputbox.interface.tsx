export interface InputboxInterface {
    value: string;
}

export interface InputboxAction {
    type: string;
    inputValue: string;
    print: any;
}

export class Commands {
    static SendCommand = 'Send_Command';
}
