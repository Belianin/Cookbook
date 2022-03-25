import React from 'react'

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

export interface Recipe {
    title: string,
    tags: Tag[],
    description: string,
    ingredients: Ingredient[]
    blocks: RecipeBlock[]
}

export interface Tag {
    category: string,
    id: string,
}

export interface Ingredient {
    name: string,
    count: number,
    units: string
}

export interface RecipeBlock {
    title: string,
    content: string,
}

export const RecipeItem: React.FC<RecipeProps> = (props) => {

    return <article className={"paper content"}>
        <header>
            <h3>{props.recipe.title}</h3>
            {props.recipe.tags.map((e, i) => {
                return <span key={i} className={"tag"}>{props.tags[e.category][e.id].title}</span>
            })}
            <p className={"blockquote"}>{props.recipe.description}</p>
            <p>Ингредиенты</p>
            <ul>
                {props.recipe.ingredients.map((e, i) => {
                    return <li key={i}>{e.name} — {e.count} {e.units}</li>
                })}
            </ul>
        </header>
        {props.recipe.blocks.map((e, i) => {
            return <section key={i}>
                <h5>{i}. {e.title}</h5>
                <p>{e.content}</p>
                <hr />
            </section>
        })}
    </article>
}