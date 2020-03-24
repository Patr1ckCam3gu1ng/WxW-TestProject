export interface InputboxInterface {
    value: string;
}

export interface InputboxAction {
    type: string;
    inputValue: string;
}

export class Commands {
    static SendCommand = 'Send_Command';
}
