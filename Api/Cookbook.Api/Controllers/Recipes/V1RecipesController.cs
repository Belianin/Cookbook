using System;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.Api.Controllers.Recipes.Models;
using Cookbook.Recipes.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook.Api.Controllers.Recipes;

[Route("v1/recipes")]
public class V1RecipesController : ControllerBase
{
    private readonly IRecipesRepository recipesRepository;

    public V1RecipesController(IRecipesRepository recipesRepository)
    {
        this.recipesRepository = recipesRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await recipesRepository.GetAllAsync();

        return Ok(result.Select(x => RecipeResponse.FromValue(x)).ToArray());
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await recipesRepository.GetByIdAsync(id);

        return Ok(RecipeResponse.FromValue(result));
    }
}