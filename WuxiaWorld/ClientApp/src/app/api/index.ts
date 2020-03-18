import axios from 'axios';
import common from '../common';

const apiRootUrl = common.apiUrl();

export default {
    get() {
        return {
            commands: (headers: string, commandType: string) => {
                return (async function() {
                    return (await axios.get(`${apiRootUrl}/${commandType}`)).data;
                })().then(data => data);
            },
        };
    },
};
