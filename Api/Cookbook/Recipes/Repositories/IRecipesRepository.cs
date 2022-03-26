using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cookbook.Recipes.Repositories;

public interface IRecipesRepository
{
    Task<ICollection<Recipe>> GetAllAsync();
    Task<Recipe> GetByIdAsync(Guid id);
}