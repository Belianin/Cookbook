import React, {useEffect, useState} from 'react';
import './App.css';
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {RecipesPage} from "./components/pages/RecipesPage";
import {RecipePage} from "./components/pages/RecipePage";
import {NavBar} from "./components/NavBar";
import {Menu} from "./components/Menu";
import {NotFoundPage} from "./components/pages/NotFoundPage";
import {RegisterPage} from "./components/pages/RegisterPage";
import {UserContext, UserContextState} from "./components/UserContext";
import {getMe} from "./ApiClient";



const App: React.FC = () => {

    const [userState, setUserState] = useState<UserContextState>({
        userId: null,
        setUser: setUser
    })

    function setUser(userId: string){
        setUserState(p => {return {...p, userId}})
    }

    useEffect(() => {
        getMe().then(r => {
            if (r.ok)
                return r.json().then(r => setUser(r.id))
        })
    }, [])

    return <div>
        <UserContext.Provider value={userState}>
            <BrowserRouter>
                <NavBar/>
                <div className={"centered"}>
                    <Routes>
                        <Route path={"/"} element={<h1>Hello</h1>}/>
                        <Route path={"/recipes"} element={<RecipesPage/>}/>
                        <Route path={"/recipes/:id"} element={<RecipePage/>}/>
                        <Route path={"/registration"} element={<RegisterPage/>}/>
                        <Route path={"*"} element={<NotFoundPage/>}/>
                    </Routes>
                </div>
            </BrowserRouter>
        </UserContext.Provider>
    </div>
}

export default App;
