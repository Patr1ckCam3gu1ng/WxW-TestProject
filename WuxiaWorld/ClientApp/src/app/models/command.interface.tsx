export interface TerminalCommands {
    list: { method: (args: any, print: any) => void };
    login: { method: (args: any, print: any) => void };
}
