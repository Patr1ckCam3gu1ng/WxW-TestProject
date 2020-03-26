import { ApiError } from '../models/apiError.interface';
import { Action } from '../models/action.interface';

export class ErrorMessage {
    static AdminRole = "Error: The 'admin' user is the only one who can write to the repo.";
    static InvalidSyntax = 'Error: Invalid syntax. Syntax should be:';
}

export default {
    throwError: (error: any): ApiError => {
        const { status, data } = error.response;
        throw Object.assign({
            code: status,
            message: data,
        } as ApiError);
    },
    errorCode: (action: Action) => {
        return function(error: ApiError) {
            if (error.code === 401) {
                action.print(ErrorMessage.AdminRole);
            }
            if (error.code === 400) {
                action.print(error.message);
            }
        };
    },
};
