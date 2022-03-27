import React, {useEffect, useState} from "react";
import {getRecipes} from "../../ApiClient";
import {Recipe} from "../../Types";
import {Link} from "react-router-dom";

interface RecipesPageState {
    recipes: Recipe[] | null
}

export const RecipesPage: React.FC = () => {
    const [state, setState] = useState<RecipesPageState>({
        recipes: null
    });

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

    useEffect(() => {
        getRecipes()
            .then(r => setState({recipes: r}))
    }, [])

    if (!state.recipes)
        return <h1>Рецептов нет</h1>

    return <div>
        {state.recipes.map((e, i) => <div key={i}><Link to={`/recipes/${e.id}`}>{e.title} — {e.description}</Link></div>)}
    </div>
}