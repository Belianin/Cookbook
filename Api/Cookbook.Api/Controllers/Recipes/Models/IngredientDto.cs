using Cookbook.Recipes;

namespace Cookbook.Api.Controllers.Recipes.Models;

public class IngredientDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public double Count { get; set; }
    public string Units { get; set; }

    public static IngredientDto FromValue(Ingredient ingredient)
    {
        return new IngredientDto
        {
            Id = ingredient.Id,
            Title = ingredient.Title,
            Count = ingredient.Count,
            Units = ingredient.Units
        };
    }
}