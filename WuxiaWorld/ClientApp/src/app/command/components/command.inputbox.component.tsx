import React from 'react';
import { NovelContext } from '../../contexts/NovelContext';
import { NovelState } from '../../models/novelState.interface';
import { AuthContext } from '../../contexts/AuthContext';
import { AuthStateInterface } from '../../models/authState.interface';

const CounterComponent = (): any => {
    return (
        <AuthContext.Consumer>
            {(authContext: Partial<AuthStateInterface>): any => (
                <NovelContext.Consumer>
                    {({ inputValue }: Partial<NovelState>): any => {
                        return <input type={'text'} value={inputValue} />;
                    }}
                </NovelContext.Consumer>
            )}
        </AuthContext.Consumer>
    );
};

export default CounterComponent;

// export default class CommandInputBox extends PureComponent {
// render = () => (
//     <div>
//         <input
//             type={'text'}
//             onKeyUp={(event: React.KeyboardEvent<HTMLInputElement>) => {
//                 if (event.keyCode === 13) {
//                     this.props.onEnter(event.currentTarget.value, this.props.actionSetState);
//                 }
//             }}
//             onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
//                 this.props.setInputValue(event.currentTarget.value);
//             }}
//             value={this.props.inputValue}
//         />
//     </div>
// );
// }
