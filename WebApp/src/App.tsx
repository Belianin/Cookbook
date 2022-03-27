import React, {useState} from 'react';
import './App.css';
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {RecipesPage} from "./components/pages/RecipesPage";
import {RecipeItem} from "./components/RecipeItem";
import {NavBar} from "./components/NavBar";

const App: React.FC = () => {
    const tags = {
        "country": {
            "korea": {
                title: "Корея"
            }
        },
        "taste": {
            "spicy": {
                title: "Острое 🔥"
            }
        }
    }

    return <div>
        <BrowserRouter>
            <NavBar/>
            <div className={"centered"}>
                <Routes>
                    <Route path={"/"} element={<h1>Hello</h1>}/>
                    <Route path={"/recipes"} element={<RecipesPage/>}/>
                    <Route path={"/recipes/:id"} element={<RecipeItem/>}/>
                </Routes>
            </div>
        </BrowserRouter>
    </div>
}

export default App;
