import split from 'split-string';

export default {
    splitQuoteString: (value: any): string[] => {
        const cleanInputs = value.toString().replace(/,/g, ' ');
        return split(cleanInputs, { quotes: ['"'], separator: ' ' });
    },
};
