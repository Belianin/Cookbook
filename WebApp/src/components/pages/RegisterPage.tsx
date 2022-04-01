import React, {useState} from 'react';
import {register} from "../../ApiClient";
import {LoginForm} from "../LoginForm";

interface RegisterPageState {
    login: string,
    password: string
}

export const RegisterPage: React.FC = () => {

    const [state, setState] = useState<RegisterPageState>({
        login: "",
        password: ""
    })

    function setLogin(login: string){
        setState(prev => {return {...prev, login}})
    }

    function setPassword(password: string){
        setState(prev => {return {...prev, password}})
    }

    return <div>
        <h1>Регистрация</h1>

        <p>Имя пользователя</p>
        <input value={state.login} onChange={x => setLogin(x.target.value) }/>
        <p>Пароль</p>
        <input value={state.password} type={"password"} onChange={x => setPassword(x.target.value)}/>
        <p>Пароль ещё раз</p>
        <input type={"password"}/>

        <br/>
        <br/>
        <button onClick={() => register(state.login, state.password)}>Зарегистрироваться</button>

        <LoginForm/>
    </div>
}