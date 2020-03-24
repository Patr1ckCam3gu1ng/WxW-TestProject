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
