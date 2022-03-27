export interface Recipe {
    id: string,
    title: string,
    // tags: Tag[],
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