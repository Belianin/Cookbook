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

export async function register(username: string, password: string): Promise<Response> {
    return fetch('/v1/users/register', {
        body: JSON.stringify({username, password}),
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        }
    })
}

export async function login(username: string, password: string): Promise<Response> {
    return fetch('/v1/users/login', {
        body: JSON.stringify({username, password}),
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        }
    })
}

export async function getMe(): Promise<Response> {
    return fetch('/v1/users/me')
}