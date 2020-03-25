import { ApiError } from '../models/apiError.interface';

export default {
    throwError: (error: any): ApiError => {
        const { status, data } = error.response;
        throw Object.assign({
            code: status,
            message: data,
        } as ApiError);
    },
};

export class ErrorMessage {
    static AdminRole = 'Error: The admin user is the only one who can write to the repo.';
    static InvalidSyntax = 'Error: Invalid syntax. Syntax should be:';
}
