using System;
using System.Threading.Tasks;
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

        return Ok(result);
    }
    
    [HttpGet("id:guid")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var result = await recipesRepository.GetByIdAsync(id);

        return Ok(result);
    }
}