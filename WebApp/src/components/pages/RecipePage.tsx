import React, {useEffect, useState} from 'react'
import {Recipe} from "../../Types";
import {useParams} from "react-router-dom";
import {getRecipe} from "../../ApiClient";
import {Loader} from "../Loader";

export interface RecipeProps {
    recipe: Recipe,
    tags: {
        [category: string]: {
            [id: string]: TagDescription
        }
    }
}

export interface TagDescription {
    title: string,
}


export const RecipePage: React.FC = () => {
    const [state, setState] = useState<Recipe | null>(null)

    const {id} = useParams();
    if (!id)
        return <h1>No id</h1>

    useEffect(() => {
        getRecipe(id)
            .then(x => setState(x))

    }, [])

    if (!state)
        return <Loader />

    return <article className={"paper content"}>
        <header>
            <h3>{state.title}</h3>
            {/*{props.recipe.tags.map((e, i) => {*/}
            {/*    return <span key={i} className={"tag"}>{props.tags[e.category][e.id].title}</span>*/}
            {/*})}*/}
            <p className={"blockquote"}>{state.description}</p>
            <p>Ингредиенты</p>
            <ul>
                {state.ingredients.map((e, i) => {
                    return <li key={i}>{e.name} — {e.count} {e.units}</li>
                })}
            </ul>
        </header>
        {state.blocks.map((e, i) => {
            return <section key={i}>
                <h5>{i}. {e.title}</h5>
                <p>{e.content}</p>
                <hr />
            </section>
        })}
    </article>
}