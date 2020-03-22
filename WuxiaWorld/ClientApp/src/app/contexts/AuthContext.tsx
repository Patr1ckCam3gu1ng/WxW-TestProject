import React, { Component, createContext } from 'react';
import { AuthStateInterface } from '../models/authState.interface';

export const AuthContext = createContext<Partial<AuthStateInterface>>({});

class AuthContextProvider extends Component {
    state: AuthStateInterface = {
        isAuthenticated: false,
        jwtToken: '',
    };

    render() {
        return <AuthContext.Provider value={{ ...this.state }}>{this.props.children}</AuthContext.Provider>;
    }
}

export default AuthContextProvider;
