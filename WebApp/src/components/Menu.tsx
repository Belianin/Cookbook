import React from "react";
import {NavLink} from "react-router-dom";

export const Menu: React.FC = () => {

    return <aside className={"paper"}>
        <NavLink to={"/recipes"}>Рецепты</NavLink>
    </aside>
}