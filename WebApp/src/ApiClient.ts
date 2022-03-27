import {Recipe} from "./Types";


export async function getRecipes(): Promise<Recipe[]> {
    return fetch("/v1/recipes")
        .then(x => {
            if (x.ok)
                return x.json()
            throw new Error(x.statusText);
        })
}

export async function getRecipe(id: string): Promise<Recipe> {
    return fetch(`/v1/recipes/${id}`)
        .then(x => {
            if (x.ok)
                return x.json()
            throw new Error(x.statusText);
        })
}