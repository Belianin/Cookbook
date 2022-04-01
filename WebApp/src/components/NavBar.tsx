import React, {useContext} from "react";
import {NavLink} from "react-router-dom";
import {UserContext} from "./UserContext";

export const NavBar: React.FC = () => {

    const userContext = useContext(UserContext);

    return <nav className={"navbar"}>
        <ul className={"links"}>
            <li>
                <header>
                    <h3>Project Angel</h3>
                </header>
            </li>
            <li>
                <NavLink to={"/recipes"}>Рецепты</NavLink>
            </li>
            <li>
                {userContext.userId ? userContext.userId : "Неавторизован"}
            </li>
        </ul>
    </nav>
}