import React, { useContext, useState } from 'react';
import { NovelContext } from '../../contexts/NovelContext';
import { InputboxAction, Commands } from '../../models/inputbox.interface';

const CounterComponent = () => {
    // @ts-ignore
    const { dispatch } = useContext(NovelContext);
    const [inputValue, setInputValue] = useState('');

    const handleSubmit = (e: any) => {
        e.preventDefault();
        dispatch({
            type: Commands.SendCommand,
            inputValue,
        } as InputboxAction);
        setInputValue('');
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                required
                value={inputValue}
                onChange={(event: React.ChangeEvent<HTMLInputElement>): void =>
                    setInputValue(event.currentTarget.value)
                }
            />
        </form>
    );
};

export default CounterComponent;
