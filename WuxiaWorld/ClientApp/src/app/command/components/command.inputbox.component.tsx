import React, { useContext } from 'react';
import Terminal from 'terminal-in-react';
import { NovelContext } from '../../contexts/NovelContext';
import { Commands, InputboxAction } from '../../models/inputbox.interface';

const CounterComponent = () => {
    // @ts-ignore
    const { dispatch } = useContext(NovelContext);

    return (
        <div
            style={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                height: '100vh',
            }}
        >
            <Terminal
                color="green"
                backgroundColor="black"
                barColor="black"
                style={{ fontWeight: 'bold', fontSize: '1em' }}
                commands={{
                    list: {
                        method: (args: any, print: any) => {
                            dispatch({
                                type: Commands.SendCommand,
                                inputValue: args._[0],
                                print: print,
                            } as InputboxAction);
                        },
                    },
                }}
                msg="Welcome to WuxiaWorld. Enter command:"
                allowTabs={false}
                commandPassThrough={(cmd, print: any) => {
                    print('Hello');
                }}
            />
        </div>
    );
};

export default CounterComponent;
