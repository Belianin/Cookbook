using System;

namespace Cookbook.Recipes;

public class Recipe
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string[] Tags { get; set; }
    public string Description { get; set; }
    public Ingredient[] Ingredients { get; set; }
    public RecipeBlock[] Blocks { get; set; }
}