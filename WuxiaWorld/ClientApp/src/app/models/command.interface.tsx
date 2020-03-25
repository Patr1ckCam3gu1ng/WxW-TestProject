export interface TerminalCommands {
    list: { method: (args: any, print: any, runCommand: any) => void };
    create: { method: (args: any, print: any, runCommand: any) => void };
    login: { method: (args: any, print: any) => void };
}
