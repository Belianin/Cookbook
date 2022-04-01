import React, {useContext, useState} from 'react';
import {login as loginApi} from '../ApiClient'
import {UserContext} from "./UserContext";

interface LoginState {
    login: string,
    password: string
}

export const LoginForm: React.FC = () => {

    const userContext = useContext(UserContext)

    const [state, setState] = useState<LoginState>({
        login: "",
        password: ""
    })

    function setLogin(login: string){
        setState(prev => {return {...prev, login}})
    }

    function setPassword(password: string){
        setState(prev => {return {...prev, password}})
    }

    function login() {
        loginApi(state.login, state.password)
            .then(x => {
                if (x.ok)
                    return x.json().then(y => userContext.setUser(y.id));
            })
    }

    return <div>
        <h1>Вход</h1>
        <p>Логин</p>
        <input value={state.login} onChange={x => setLogin(x.target.value)} />
        <p>Пароль</p>
        <input value={state.password} type={"password"} onChange={x => setPassword(x.target.value)} />
        <br />
        <br />
        <button onClick={() => login()}>Войти</button>
    </div>
}