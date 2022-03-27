import React from "react";
import {NavLink} from "react-router-dom";

export const NavBar: React.FC = () => {

    return <nav>
        <header>
            <h3>Project Angel</h3>
        </header>
        <NavLink to={"/recipes"}>Рецепты</NavLink>
    </nav>
}