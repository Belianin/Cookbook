using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.Common;
using Newtonsoft.Json;

namespace Cookbook.Recipes.Repositories;

public class JsonFileRecipesRepository : IRecipesRepository
{
    private readonly SharedDirectoryWatcher directoryWatcher;
    private Dictionary<Guid, Recipe> recipes;

    public JsonFileRecipesRepository(SharedDirectoryWatcher directoryWatcher, string filename)
    {
        this.directoryWatcher = directoryWatcher;
        
        this.directoryWatcher = directoryWatcher;
        directoryWatcher.Subscribe(filename, x => recipes = ReadFile(x));
        recipes = ReadFile(Path.Combine(directoryWatcher.Directory, filename));
    }
    
    public Task<ICollection<Recipe>> GetAllAsync()
    {
        return Task.FromResult((ICollection<Recipe>) recipes.Values);
    }

    public Task<Recipe> GetByIdAsync(Guid id)
    {
        return Task.FromResult(recipes[id]);
    }
    
    private static Dictionary<Guid, Recipe> ReadFile(string fullPath)
    {
        var text = File.ReadAllText(fullPath);

        return JsonConvert.DeserializeObject<Recipe[]>(text)!.ToDictionary(x => x.Id, y => y);
    }
}