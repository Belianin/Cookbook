using System;
using System.Linq;
using Cookbook.Recipes;

namespace Cookbook.Api.Controllers.Recipes.Models;

public class RecipeResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string[] Tags { get; set; }
    public string Description { get; set; }
    public IngredientDto[] Ingredients { get; set; }
    public RecipeBlock[] Blocks { get; set; }

    public static RecipeResponse FromValue(Recipe recipe)
    {
        return new RecipeResponse
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Tags = recipe.Tags,
            Description = recipe.Description,
            Ingredients = recipe.Ingredients.Select(x => IngredientDto.FromValue(x)).ToArray(),
            Blocks = recipe.Blocks
        };
    }
}